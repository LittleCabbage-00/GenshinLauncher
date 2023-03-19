﻿using System;
using System.Collections;
using System.IO;
using System.Windows;

namespace Genshin_Launcher.Core
{
    public class IniParser
    {
        private Hashtable keyPairs = new();
        private string iniFilePath;

        private struct SectionPair
        {
            public string Section;
            public string Key;
        }

        public IniParser(string iniPath)
        {
            TextReader iniFile = null;
            string strLine = null;
            string currentRoot = null;
            string[] keyPair = null;
            iniFilePath = iniPath;
            if (File.Exists(iniPath))
            {
                try
                {
                    iniFile = new StreamReader(iniPath);
                    strLine = iniFile.ReadLine();
                    while (strLine != null)
                    {
                        strLine = strLine.Trim();
                        if (strLine != string.Empty)
                        {
                            if (strLine.StartsWith("[") && strLine.EndsWith("]"))
                            {
                                currentRoot = strLine.Substring(1, strLine.Length - 2);
                            }
                            else
                            {
                                keyPair = strLine.Split(new char[] { '=' }, 2);
                                SectionPair sectionPair;
                                string value = null;
                                if (currentRoot == null)
                                    currentRoot = "ROOT";
                                sectionPair.Section = currentRoot;
                                sectionPair.Key = keyPair[0];
                                if (keyPair.Length > 1)
                                    value = keyPair[1];
                                keyPairs.Add(sectionPair, value);
                            }
                        }
                        strLine = iniFile.ReadLine();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error : {ex.Message}");
                }
                finally
                {
                    if (iniFile != null)
                        iniFile.Close();
                }
            }
            else
            {
                if (!Directory.Exists(@"Config"))
                {
                    Directory.CreateDirectory("Config");
                }
            }
        }

        /// <summary>
        /// 根据类型返回相应内容
        /// </summary>
        public string GetSetting(string sectionName, string settingName, int i=0)
        {
            SectionPair sectionPair;
            sectionPair.Section = sectionName;
            sectionPair.Key = settingName;
            switch (i)
            {
                case 1:
                    return (string)keyPairs[sectionPair] != string.Empty ? (string)keyPairs[sectionPair] : "1";
                case 2:
                    return (string)keyPairs[sectionPair] != string.Empty ? (string)keyPairs[sectionPair] : "False";
                default:
                    return (string)keyPairs[sectionPair];
            }

        }

        public void AddSetting(string sectionName, string settingName, string settingValue)
        {
            SectionPair sectionPair;
            sectionPair.Section = sectionName;
            sectionPair.Key = settingName;
            if (keyPairs.ContainsKey(sectionPair))
                keyPairs.Remove(sectionPair);
            keyPairs.Add(sectionPair, settingValue);
        }

        public void SaveSettings()
        {
            ArrayList sections = new();
            string tmpValue = "";
            string strToSave = "";
            foreach (SectionPair sectionPair in keyPairs.Keys)
            {
                if (!sections.Contains(sectionPair.Section))
                    sections.Add(sectionPair.Section);
            }
            foreach (string section in sections)
            {
                strToSave += ("[" + section + "]\r\n");
                foreach (SectionPair sectionPair in keyPairs.Keys)
                {
                    if (sectionPair.Section == section)
                    {
                        tmpValue = (string)keyPairs[sectionPair];
                        if (tmpValue != null)
                            tmpValue = "=" + tmpValue;
                        strToSave += (sectionPair.Key + tmpValue + "\r\n");
                    }
                }
                strToSave += "\r\n";
            }
            try
            {
                TextWriter tw = new StreamWriter(iniFilePath);
                tw.Write(strToSave);
                tw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error : {ex.Message}");
            }
        }
    }
}
