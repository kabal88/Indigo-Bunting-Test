using System.Collections.Generic;

namespace CodeGenerator
{
    public interface ISyntax
    {
        string StringValue { get; set; }
        List<ISyntax> Tree { get; set; }
    }
   
    public class TreeSyntaxNode : ISyntax, IRawSyntaxData
    {
        public List<ISyntax> Tree { get; set; } = new List<ISyntax>();
        public string StringValue { get; set; } = "MainTree";
        public string[] RawData { get; set; }

        public void Add(ISyntax syntax)
        {
            Tree.Add(syntax);
        }

        public override string ToString()
        {
            var t = string.Empty;

            for (int i = 0; i < Tree.Count; i++)
            {
                t += Tree[i].ToString();
            }

            return t;
        }
    }

    public interface IRawSyntaxData
    {
        string[] RawData { get; set; }
    }
    
    public class SyntaxNode : ISyntax
    {
        public virtual string StringValue { get; set; }
        public List<ISyntax> Tree { get; set; } = new List<ISyntax>(8);
    }

    public class SimpleSyntax : SyntaxNode
    {
        public string Data;

        public SimpleSyntax() { }

        public SimpleSyntax(string data)
        {
            Data = data;
        }

        public override string ToString()
        {
            return Data;
        }
    }

    public class UsingSyntax : SyntaxNode
    {
        public string Name;
        private int paragraphCount = 1;

        public UsingSyntax(string name)
        {
            Name = name;
        }

        public UsingSyntax(string name, int paragraphAfter) : this(name)
        {
            paragraphCount += paragraphAfter;
        }

        public override string StringValue { get; set; } = CParse.Using;

        public override string ToString()
        {
            var s = string.Empty;
            s = StringValue + CParse.Space + Name + CParse.Semicolon;

            for (int i = 0; i < paragraphCount; i++)
            {
                s += CParse.Paragraph;
            }

            return s;
        }
    }

    public class LeftScopeSyntax : ISyntax
    {
        public bool IsWithoutParagraph;

        public LeftScopeSyntax()
        {
        }

        public LeftScopeSyntax(int tabSpace)
        {
            StringValue = string.Empty;

            for (int i = 0; i < tabSpace; i++)
            {
                StringValue += CParse.Tab;
            }

            StringValue += CParse.LeftScope;
        }

        public LeftScopeSyntax(int tabspace, bool isWithoutParagraph) : this(tabspace)
        {
            IsWithoutParagraph = isWithoutParagraph;
        }

        public List<ISyntax> Tree { get; set; } = new List<ISyntax>();
        public string StringValue { get; set; } = CParse.LeftScope;

        public override string ToString()
        {
            return StringValue + (IsWithoutParagraph ? "" : CParse.Paragraph);
        }
    }

    public class RightScopeSyntax : ISyntax
    {
        public List<ISyntax> Tree { get; set; } = new List<ISyntax>();
        public string StringValue { get; set; } = CParse.RightScope;
        public bool IsCommaNeeded;

        public RightScopeSyntax(int tabSpace)
        {
            StringValue = string.Empty;

            for (int i = 0; i < tabSpace; i++)
            {
                StringValue += CParse.Tab;
            }

            StringValue += CParse.RightScope;
        }

        public RightScopeSyntax(int tabspace, bool isClosed) : this(tabspace)
        {
            if (!isClosed)
            {
                StringValue += CParse.Comma;
                return;
            }
                
            StringValue += CParse.Semicolon;
        }

        public RightScopeSyntax()
        {
        }

        public override string ToString()
        {
            if (IsCommaNeeded)
                return StringValue + CParse.Comma + CParse.Paragraph;

            return StringValue + CParse.Paragraph;
        }
    }
    
    public class TabSimpleSyntax : ISyntax
    {
        public string StringValue { get; set; }
        public List<ISyntax> Tree { get; set; }
    
        private int index;
    
        public TabSimpleSyntax(int tab, string data)
        {
            StringValue = data;
            index = tab;
        }
    
        public override string ToString()
        {
            var data = string.Empty;
    
            for (int i = 0; i < index; i++)
            {
                data += CParse.Tab;
            }
    
            data += StringValue;
            data += CParse.Paragraph;
    
            return data;
        }
    }
}