using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace AutoFixture
{
    class Operator:INotifyPropertyChanged
    {
        private string operator_ID= "";
        private List<string> AuthorizedList;
        public int StepNumber { get; set; } = 0;
        public string OrderNumber { get; set; } = "";
        public string ProductNumber { get; set; } = "";
        
        public string OperID
        {
            get { return operator_ID; }
            set {
                    operator_ID = value;
                    if(PropertyChanged!=null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("OperID"));
                }
            }
        }


        public void Init()
        {
            operator_ID = "";
            AuthorizedList = null;
            StepNumber = 0;
            OrderNumber = "";
            ProductNumber = "";

        }

        public void GotoStep1()
        {
            StepNumber = 1;

        }
        public event PropertyChangedEventHandler PropertyChanged;


    }
}
