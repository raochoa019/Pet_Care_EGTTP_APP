using System;
using System.Collections.Generic;
using System.Text;

namespace PRY_LENG_PROG.Modelos
{
    internal class ViewModel
    {
        public List<ChartInfoModel> DataWeight { get; set; }
        public List<ChartInfoModel> DataHeight { get; set; }
        private List<string> months; 

        public ViewModel(List<ReservationModel> data)
        {
            months = new List<string>() { "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic" };
            DataWeight = new List<ChartInfoModel>();
            DataHeight = new List<ChartInfoModel>();
            initializeData();         
            transformInfo(data);
        }

        private void transformInfo(List<ReservationModel> data)
        {
            foreach (ReservationModel model in data)
            {
                string[] infoDate = model.date.Split(' ')[0].Split('-');
                int anio = Convert.ToInt32(infoDate[0]);
                if(anio == DateTime.Now.Year)
                {
                    int mes = Convert.ToInt32(infoDate[1]);
                    DataWeight[mes - 1].info = model.weight;
                    DataHeight[mes - 1].info = model.height;
                }                      
            }
        }

        private void initializeData()
        {
            for(int i = 0; i < months.Count; i++)
            {
                this.DataWeight.Add(new ChartInfoModel { month = this.months[i], info = 0.0 });
                this.DataHeight.Add(new ChartInfoModel { month = this.months[i], info = 0.0 });
            }
        }
    }
}
