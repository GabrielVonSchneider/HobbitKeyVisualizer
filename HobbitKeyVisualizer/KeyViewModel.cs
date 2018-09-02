using HobbitKeyVisualizer.Common;

namespace HobbitKeyVisualizer
{
    internal class KeyViewModel : PropertyChangedBase
    {
        private bool isDown;

        public bool IsDown
        {
            get => this.isDown;
            set => this.Set(ref this.isDown, value);
        }

        public class Token
        {
            public KeyViewModel Vm { get; }

            public Token()
            {
                this.Vm = new KeyViewModel();
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