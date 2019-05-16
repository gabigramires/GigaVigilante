using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools
{
    public interface IProgressBarHandler
    {
        void Setup(int Min, int Max);
        void Write(int value);
        void Clear();
    }

    public interface ITextInfo
    {
        void Add(Images image, string text);
        void ChangeLastItem(Images image);
        void ChangeLastItem(string text);
    }

}
