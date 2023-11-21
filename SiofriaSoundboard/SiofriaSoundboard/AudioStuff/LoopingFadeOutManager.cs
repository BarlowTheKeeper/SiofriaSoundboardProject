using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiofriaSoundboard.AudioStuff
{
    internal class LoopingFadeOutManager
    {
        private ConcurrentDictionary<SoundClip, float> fadeoutClips;
        private int fadeOutRate; //ms
        private Thread thread = null;

        public LoopingFadeOutManager(int fadeOutRate = 100) 
        {
            fadeoutClips = new ConcurrentDictionary<SoundClip, float>();
            this.fadeOutRate = fadeOutRate;
        }

        public void FadeOutThread()
        {
            while (true)
            {
                List<KeyValuePair<SoundClip, float>> doneList = new ();
                foreach (var clip in fadeoutClips)
                {
                    VolumeSampleProvider volSampler = clip.Key.GetVolumeSampleProvider();

                    if (volSampler.Volume <= 0)
                    {
                        doneList.Add(clip);
                        continue;
                    }

                    if (volSampler.Volume < clip.Value)
                        volSampler.Volume = 0;
                    else
                        volSampler.Volume -= clip.Value;
                }

                foreach (var clipPair in doneList)
                {
                    fadeoutClips.TryRemove(clipPair);
                    SoundClip clip = clipPair.Key;
                    clip.GetVolumeSampleProvider().Volume = clip.Volume;
                    clip.Dispose();
                }

                Thread.Sleep(fadeOutRate);
            }
        }

        public void QueueClipForFadeout(SoundClip clip)
        {
            float fadeoutTime = clip.FadeOutAmount*1000.0f;
            float fadeoutSteps = fadeoutTime / fadeOutRate;
            float amountEachStep = clip.GetVolumeSampleProvider().Volume / fadeoutSteps;

            fadeoutClips.TryAdd(clip, amountEachStep);

            if(thread == null)
            {
                thread = new Thread(FadeOutThread);
                thread.Start();
            }
        }

        public void CancelClipFadeout(SoundClip clip)
        {

        }
    }
}
