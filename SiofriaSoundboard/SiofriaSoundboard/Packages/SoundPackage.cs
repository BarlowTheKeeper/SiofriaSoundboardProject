using Newtonsoft.Json;
using SiofriaSoundboard.AudioStuff;
using SiofriaSoundboard.Input;
using SiofriaSoundboard.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiofriaSoundboard.Packages
{
    public class SoundPackage
    {
        private DictionaryBindingList<KeyPress, SoundClip> soundBindings = null;
        //private string config;
        public string Config { get; private set; } = "";
        public bool Valid { get; private set; } = true;

        public SoundPackage() 
        {
            soundBindings = new DictionaryBindingList<KeyPress, SoundClip>();
        }

        public SoundPackage(string configPath = "") 
        {
            soundBindings = new DictionaryBindingList<KeyPress, SoundClip>();

            if (configPath.Length == 0)
                return;
            Config = configPath;
            Load();
        }

        private void Load()
        {
            try
            {
                foreach (string line in File.ReadLines(Config))
                {
                    int kyecode = Int32.Parse(line.Split("][")[0].Substring(1));
                    KeyPress key = new KeyPress(kyecode);

                    string clip_json = line.Split("]:")[1].Trim();
                    SoundClip clip = JsonConvert.DeserializeObject<SoundClip>(clip_json);

                    soundBindings.Add(key, clip);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load that file, check the logs! " + ex.Message);
                Log.Write(ex);
                Valid = false;
            }
        }

        public void SaveAs(string configPath) 
        {
            Config = configPath;
            Save();
        }

        public void Save()
        {
            if (Config.Length == 0)
                throw new ArgumentException("Can't save this package Config is empty!");


            using (StreamWriter writer = new StreamWriter(Config))
            {
                foreach (var row in soundBindings)
                {
                    string json = row.Value.DumpSoundClipInfo();
                    writer.WriteLine("[" + row.Key.keycode.ToString() + "][" + row.Key.ToString() + "]:" + json);
                }
            }
        }

        public void SerializeConfigForExport(string configDumpLocation, string newAudioLocations)
        {
            if (newAudioLocations.Length == 0)
                throw new ArgumentException("newAudioLocation can not be empty!");

            using (StreamWriter writer = new StreamWriter(configDumpLocation))
            {
                foreach (var row in soundBindings)
                {
                    SoundClip clip = row.Value;

                    SoundClip clipClone = (SoundClip)clip.Clone();
                    clipClone.Filepath = Path.Combine(newAudioLocations, Path.GetFileName(clipClone.Filepath));

                    string json = clipClone.DumpSoundClipInfo();
                    writer.WriteLine("[" + row.Key.keycode.ToString() + "][" + row.Key.ToString() + "]:" + json);
                }
            }
        }

        public DictionaryBindingList<KeyPress, SoundClip> GetSoundBindings()
        {
            return soundBindings;
        }

        public List<string> GetAllSounds()
        {
            List<string> list = new List<string>();
            foreach (var item in soundBindings)
                list.Add(item.Value.Filepath);
            return list;
        }
    }
}
