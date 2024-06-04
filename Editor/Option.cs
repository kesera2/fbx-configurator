namespace kesera2.FBXOptionsManager
{


    internal class Option<T>
    {
        private T _value { get; set; }
        private int _toolbarEnable = 0;
        private string _label;
        private string _tooltip;
        internal Option(T value, int toolbarEnable, string label, string tooltip = "")
        {
            _value = value;
            _toolbarEnable = toolbarEnable;
            _label = label;
            _tooltip = tooltip;
        }
        public T Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public int ToolbarEnable
        {
            get { return _toolbarEnable; }
            set { _toolbarEnable = value; }
        }

        public string Label
        {
            get { return _label; }
        }

        public string Tooltip
        {
            get { return _tooltip; }
        }
    }
}