﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using Pickles.Parser;

namespace Pickles
{
    [DebuggerDisplay("Name = {Name}")]
    public class FeatureNode
    {
        public FileSystemInfo Location 
        { 
            get; 
            set; 
        }

        public Uri Url 
        { 
            get; 
            set; 
        }

        public string RelativePathFromRoot 
        { 
            get; 
            set; 
        }

        public string Name
        {
            get
            {
                if (IsDirectory) return Location.Name;
                return Location.Name.Replace(Location.Extension, string.Empty);
            }
        }

        public Feature Feature 
        { 
            get; 
            set; 
        }

        public bool IsDirectory 
        { 
            get 
            { 
                return Location is DirectoryInfo; 
            } 
        }

        public bool IsContent
        {
            get
            {
                return !IsDirectory;
            }
        }

        public bool IsEmpty 
        { 
            get 
            { 
                return IsDirectory ? !((Location as DirectoryInfo).GetFileSystemInfos().Any()) : true; 
            } 
        }

        public FeatureNodeType Type
        {
            get
            {
                if (IsDirectory) return FeatureNodeType.Directory;

                var file = Location as FileInfo;
                if (file.Extension == ".feature") return FeatureNodeType.Feature;
                else if (file.Extension == ".md") return FeatureNodeType.Markdown;
                else return FeatureNodeType.Unknown;
            }
        }

        public string GetRelativeUriTo(Uri other, string newExtension)
        {
            return this.Location.FullName != other.LocalPath ? other.MakeRelativeUri(this.Url).ToString().Replace(this.Location.Extension, newExtension) : "#";
        }

        public string GetRelativeUriTo(Uri other)
        {
            return GetRelativeUriTo(other, ".xhtml");
        }

        public FeatureNodeType Type
        {
            get
            {
                if (IsDirectory) return FeatureNodeType.Directory;

                var file = Location as FileInfo;
                if (file.Extension == ".feature") return FeatureNodeType.Feature;
                else if (file.Extension == ".md") return FeatureNodeType.Markdown;
                else return FeatureNodeType.Unknown;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
