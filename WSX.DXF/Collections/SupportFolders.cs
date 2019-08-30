#region WSX.DXF library, Copyright (C) 2009-2018 Daniel Carvajal (haplokuon@gmail.com)

//                        WSX.DXF library
// Copyright (C) 2009-2018 Daniel Carvajal (haplokuon@gmail.com)
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace WSX.DXF.Collections
{
    /// <summary>
    /// Represents a list of support folders for the document.
    /// </summary>
    public class SupportFolders :
        IList<string>
    {
        #region private fields

        private readonly List<string> folders;

        #endregion

        #region constructors

        public SupportFolders()
        {
            this.folders = new List<string>();
        }

        public SupportFolders(int capacity)
        {
            this.folders = new List<string>(capacity);
        }

        public SupportFolders(IEnumerable<string> folders)
        {
            if (folders == null)
                throw new ArgumentNullException(nameof(folders));
            this.folders = new List<string>();
            this.AddRange(folders);
        }

        #endregion

        #region public methods

        public string FindFile(string file)
        {
            if(File.Exists(file)) return file;
            string name = Path.GetFileName(file);
            foreach (string folder in this.folders)
            {
                string newFile = string.Format("{0}{1}{2}", folder, Path.DirectorySeparatorChar, name);
                if (File.Exists(newFile)) return newFile;
            }

            return string.Empty;
        }

        #endregion

        #region implements IList<string>

        public string this[int index]
        {
            get { return this.folders[index]; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException(nameof(value));
                this.folders[index] = value;
            }
        }

        public int Count
        {
            get { return this.folders.Count; }
        }

        public bool IsReadOnly
        {
            get {return false;}
        }

        public IEnumerator<string> GetEnumerator()
        {
            return this.folders.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.folders.GetEnumerator();
        }

        public void Add(string item)
        {
            if(string.IsNullOrEmpty(item))
                throw new ArgumentNullException(nameof(item));
            this.folders.Add(item);
        }

        public void AddRange(IEnumerable<string> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            foreach (string s in collection)
            {
                this.folders.Add(s);
            }
        }

        public void Clear()
        {
            this.folders.Clear();
        }

        public bool Contains(string item)
        {
            if (string.IsNullOrEmpty(item))
                throw new ArgumentNullException(nameof(item));
            return this.folders.Contains(item);
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            this.folders.CopyTo(array, arrayIndex);
        }

        public bool Remove(string item)
        {
            if (string.IsNullOrEmpty(item))
                throw new ArgumentNullException(nameof(item));
            return this.folders.Remove(item);
        }

        public int IndexOf(string item)
        {
            return this.folders.IndexOf(item);
        }

        public void Insert(int index, string item)
        {
            this.folders.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            this.folders.RemoveAt(index);
        }

        #endregion
    }
}