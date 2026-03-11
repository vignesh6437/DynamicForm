using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicForm
{
    public class FormConfig
    {
        public string formTitle { get; set; }
        public List<ControlConfig> controls { get; set; }
    }

    public class ControlConfig
    {
        public string type { get; set; }
        public string name { get; set; }
        public string label { get; set; }
        public bool required { get; set; }
        public string text { get; set; }
        public List<string> items { get; set; }
    }
}
