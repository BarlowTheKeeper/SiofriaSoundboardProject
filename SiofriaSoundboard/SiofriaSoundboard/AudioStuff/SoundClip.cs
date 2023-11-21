using Microsoft.VisualBasic.Devices;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using Newtonsoft.Json;
using SiofriaSoundboard.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;


namespace SiofriaSoundboard.AudioStuff
{
    public class SoundClip : ICloneable
    {
        static LoopingFadeOutManager fadeoutManager = new LoopingFadeOutManager(100);

        public string Filepath { get; set; } = "";

        public float Volume { get; set; } = 1.0f;
        public bool Loop { get; set; }
        public bool Stream { get; set; } = true;

        public bool FadeInEnabled { get; set; }
        public bool FadeOutEnabled { get; set; }
        public float FadeInAmount { get; set; }
        public float FadeOutAmount { get; set; }

        public float CutRangeBegin { get; set; } //Restirict value ranges
        public float CutRangeTake { get; set; }
        public bool CutRangeEnabled { get; set; }

        private WaveOut waveOut = null;
        private VolumeSampleProvider volumeSampler;
        private MemoryStream inmemoryStream = null;

        public VolumeSampleProvider GetVolumeSampleProvider()
        {
            return volumeSampler;
        }

        private ISampleProvider PlayLooping(WaveStream file)
        {
            ISampleProvider output = file.ToSampleProvider();
            LoopStream loopy = new LoopStream(file);

            if(CutRangeEnabled)
            {
                //Not supported yet for looping files
            }

            if (FadeInEnabled)
            {
            }

            return loopy.ToSampleProvider();
        }

        private ISampleProvider PlayOnce(WaveStream file)
        {
            ISampleProvider output = file.ToSampleProvider();
            TimeSpan totalTime = file.TotalTime;

            if (CutRangeEnabled)
            {
                output = TrimAudio(output, totalTime);
                totalTime -= TimeSpan.FromMilliseconds(CutRangeBegin * 1000.0f);

                if (CutRangeTake > 0)
                    totalTime = TimeSpan.FromMilliseconds((CutRangeTake > totalTime.TotalSeconds ? totalTime.TotalSeconds : CutRangeTake) * 1000.0f);
            }

            if (FadeInEnabled || FadeOutEnabled)
                output = Fade(output, totalTime);

            return output;
        }

        public void Play()
        {
            if (waveOut != null && waveOut.PlaybackState == PlaybackState.Playing)
                return;

            if (Filepath.Length == 0 || !File.Exists(Filepath))
                return;

            try
            {
                waveOut = new();

                WaveStream stream;
                if(Stream)
                    stream = new AudioFileReader(Filepath);
                else
                {
                    if (inmemoryStream == null)
                    {
                        inmemoryStream = new MemoryStream();
                        WaveFileWriter.WriteWavFileToStream(inmemoryStream, new AudioFileReader(Filepath));
                        
                    }
                    inmemoryStream.Position = 0;
                    stream = new WaveFileReader(inmemoryStream);
                }

                ISampleProvider output;
                if (Loop)
                    output = PlayLooping(stream);
                else
                    output = PlayOnce(stream);


                MarksSoftLimiter limiterOut = new MarksSoftLimiter(output);
                limiterOut.Boost.CurrentValue = 5.9f;

                volumeSampler = new VolumeSampleProvider(limiterOut);
                volumeSampler.Volume = Volume;

                waveOut.Init(volumeSampler);
                waveOut.Play();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Audio playback failed, check the logs");
                Log.Write("Error playing clip: " + ex + " CLIP INFO: " + DumpSoundClipInfo());
            }
        }

        public void AddOnPlayBackStop(EventHandler<StoppedEventArgs> callback)
        {
            if (waveOut == null)
                return;

            waveOut.PlaybackStopped += callback;
        }

        private DelayFadeOutSampleProvider Fade(ISampleProvider sample, TimeSpan totalTime)
        {
            var faded = new DelayFadeOutSampleProvider(sample);

            if (FadeInEnabled)
                faded.BeginFadeIn(FadeInAmount * 1000.0f);
            if (FadeOutEnabled)
                faded.BeginFadeOut(totalTime.TotalMilliseconds - FadeOutAmount * 1000.0f, FadeOutAmount * 1000.0f);
            return faded;
        }

        private OffsetSampleProvider TrimAudio(ISampleProvider sample, TimeSpan totalTime)
        {

            var trimmed = new OffsetSampleProvider(sample);

            int skipOverMiliseconds = (int)(CutRangeBegin * 1000.0f);
            int takeMiliseconds = (int)(CutRangeTake * 1000.0f);

            trimmed.SkipOver = TimeSpan.FromMilliseconds(skipOverMiliseconds);
            trimmed.Take = TimeSpan.FromMilliseconds(takeMiliseconds);

            return trimmed;
        }


        public void Stop()
        {
            if (waveOut == null)
                return;
            if (waveOut.PlaybackState == PlaybackState.Stopped)
                return;

            if(Loop && FadeOutEnabled)
            {
                fadeoutManager.QueueClipForFadeout(this);
                return;
            }

            Dispose();
        }

        public void Dispose()
        {
            if (waveOut == null)
                return;

            waveOut.Stop();
            waveOut.Dispose();
        }

        public bool IsPlaying()
        {
            if (waveOut == null)
                return false;
            return waveOut.PlaybackState == PlaybackState.Playing;
        }

        public override string ToString() => Path.GetFileName(Filepath);

        public string DumpSoundClipInfo()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }

        public object Clone()
        {
            SoundClip clone = new SoundClip();

            clone.Filepath = Filepath;
            clone.Volume = Volume;
            clone.Loop = Loop;
            clone.FadeInEnabled = FadeInEnabled;
            clone.FadeOutEnabled = FadeOutEnabled;
            clone.FadeInAmount = FadeInAmount;
            clone.FadeOutAmount = FadeOutAmount;
            clone.CutRangeBegin = CutRangeBegin;
            clone.CutRangeTake = CutRangeTake;
            clone.CutRangeEnabled = CutRangeEnabled;

            return clone;
        }
    }
}
