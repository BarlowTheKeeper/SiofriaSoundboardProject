using Microsoft.VisualBasic.Devices;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;


namespace SiofriaSoundboard
{
    public class SoundClip : ICloneable
    {
        public string Filepath { get; set; } = "";

        public float Volume {get; set;}
        public bool Loop {get; set;}

        public bool FadeInEnabled {get; set;}
        public bool FadeOutEnabled {get; set;}
        public float FadeInAmount {get; set;}
        public float FadeOutAmount {get; set;}

        public float CutRangeBegin {get; set;} //Restirict value ranges
        public float CutRangeTake {get; set;}
        public bool CutRangeEnabled {get; set;}

        private WaveOut waveOut = null;

        public void Play()
        {
            if (waveOut != null && (waveOut.PlaybackState == PlaybackState.Playing))
                return;

            if (Filepath.Length == 0 || !File.Exists(Filepath))
                return;

            waveOut = new();
            ISampleProvider file = new AudioFileReader(Filepath);
            
            TimeSpan totalTime = ((AudioFileReader)file).TotalTime;
            if (CutRangeEnabled)
            {
                file = TrimAudio(file, totalTime);
                totalTime -= TimeSpan.FromMilliseconds((CutRangeBegin) * 1000.0f);
                
                if(CutRangeTake > 0)
                    totalTime = TimeSpan.FromMilliseconds((CutRangeTake>totalTime.TotalSeconds?totalTime.TotalSeconds:CutRangeTake) * 1000.0f);
            }
            
            if (FadeInEnabled || FadeOutEnabled)
            {
                file = Fade(file, totalTime);
            }
            if(Loop)
            {
                var waveProvider = file.ToWaveProvider();
                var memoryStream = new MemoryStream();
                WaveFileWriter.WriteWavFileToStream(memoryStream, waveProvider);
                memoryStream.Position = 0; // Important for WaveFileReader to be able to read the header chunk bytes
                var waveReader = new WaveFileReader(memoryStream);
                LoopStream loopy = new LoopStream(waveReader);

                waveOut.Init(loopy);
            }
            else
            {
                waveOut.Init(file);
            }
            waveOut.Volume = Volume;
            waveOut.Play();
        }


        private DelayFadeOutSampleProvider Fade(ISampleProvider sample, TimeSpan totalTime)
        {
            var faded = new DelayFadeOutSampleProvider(sample);

            if(FadeInEnabled)
                faded.BeginFadeIn(FadeInAmount*1000.0f);
            if(FadeOutEnabled)
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

            waveOut.Stop();
        }

        public bool IsPlaying()
        {
            if (waveOut == null)
                return false;
            return waveOut.PlaybackState == PlaybackState.Playing;
        }

        public override string ToString() => Path.GetFileName(Filepath);

        public object Clone()
        {
            SoundClip clone = new SoundClip();

            clone.Filepath        = Filepath;
            clone.Volume          = Volume;
            clone.Loop            = Loop;
            clone.FadeInEnabled   = FadeInEnabled;
            clone.FadeOutEnabled  = FadeOutEnabled;
            clone.FadeInAmount    = FadeInAmount;
            clone.FadeOutAmount   = FadeOutAmount;
            clone.CutRangeBegin   = CutRangeBegin;
            clone.CutRangeTake    = CutRangeTake;
            clone.CutRangeEnabled = CutRangeEnabled;

            return clone;
        }
    }
}
