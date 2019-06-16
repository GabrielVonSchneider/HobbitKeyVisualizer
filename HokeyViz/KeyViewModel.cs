using HokeyViz.Common;

namespace HokeyViz
{
    internal class KeyViewModel : PropertyChangedBase
    {
        private bool isDown;

        public KeyViewModel(string name)
        {
            this.Name = name;
        }

        public bool IsDown
        {
            get => this.isDown;
            set => this.Set(ref this.isDown, value);
        }

        public string Name { get; }

        public class Token
        {
            public KeyViewModel Vm { get; }

            public Token(string name)
            {
                this.Vm = new KeyViewModel(name);
            }

            public void Push()
            {
                this.Vm.IsDown = true;
            }

            public void Release()
            {
                this.Vm.IsDown = false;
            }
        }
    }
}