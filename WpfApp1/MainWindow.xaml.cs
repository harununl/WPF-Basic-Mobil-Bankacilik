using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();



        }

        public class Atm
        {

            public double bakiye { get; set; }
            public double miktar { get; set; }
            public double credit { get; set; }
            public double borc { get; set; }
           
         

            public Atm(double bakiye,double miktar,double credit,double borc )
            {
                this.bakiye = bakiye;
                this.miktar = miktar;
                this.credit = credit;   
                this.borc = borc;
              
            }

            public double paraYatir()
            {
                bakiye += miktar;

                
                return bakiye;
            }

            public double paraCek()
            {
                bakiye -= miktar;

                return bakiye;
            }

            public double sorgula()
            {
                return bakiye;
            }

            public double artiPara()
            {

                credit -= miktar;


                return  credit;

            }

            public double  borcOde()
            {
                credit += miktar;


                return credit;
            }
          
            public double krediCek()
            {
                borc -= miktar;



                return borc;
            }
            public double krediOde()
            {
                borc += miktar;

                return borc;
            }




        }

        private void btnYatir_Click(object sender, RoutedEventArgs e)
        {
            double miktariniz;
            double bakiyeniz;
            double credit = 1000;
            double borc;

            double.TryParse(txtYatir.Text, out miktariniz);
            double.TryParse(txtBakiye.Text, out bakiyeniz);
            double.TryParse(txtKredi.Text, out borc);



            Atm islem = new Atm(bakiyeniz, miktariniz, credit,borc);


            if (MessageBox.Show("Islemi Onayliyor Musunuz?", "UYARI!!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                double total = islem.paraYatir();

                txtBakiye.Text = total.ToString();
            }
               

            txtYatir.Clear();
           
        }



        private void btnCek_Click(object sender, RoutedEventArgs e)
        {

            double miktariniz;
            double bakiyeniz;
            double ekstra;
            double borc;

            double.TryParse(txtYatir.Text, out miktariniz);
            double.TryParse(txtBakiye.Text, out bakiyeniz);

            double.TryParse(txtArti.Text, out ekstra);
            double.TryParse(txtKredi.Text, out borc);



            Atm islem = new Atm(bakiyeniz, miktariniz,ekstra, borc);
            double total;
            double creditBalance;

            if (bakiyeniz == 0 || bakiyeniz < 0)
            {
                MessageBox.Show("Hesabinizda bakiye bulunmamaktadir");
                if (MessageBox.Show("Arti bakiyenizden kullanmak ister misiniz?", "UYARI!!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    creditBalance = islem.artiPara();

                    if (miktariniz > ekstra)
                    {
                        MessageBox.Show("Arti para limitiniz yetersiz yalnizca " + txtArti.Text + " kadar kullanabílirsiniz", "Uyari!!");
                        txtYatir.Clear();
                    }
                    else
                    {
                       
                        txtArti.Text = creditBalance.ToString();

                       MessageBox.Show("Arti bakiye kullandiniz");
                    }

                   

                }
                txtYatir.Clear();
            }
            else
            {
                 total = islem.paraCek();
                if (total >= 0)
                {
                    txtBakiye.Text = total.ToString();

                    txtYatir.Clear();
                }
                else
                {
                    MessageBox.Show("Bakiyeniz yeterli degil");

                   

                    if (MessageBox.Show("Arti bakiyenizden kullanmak ister misiniz?", "UYARI!!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        creditBalance = islem.artiPara();

                        if (miktariniz > ekstra)
                        {
                            MessageBox.Show("Arti para limitiniz yetersiz yalnizca " + txtArti.Text + " kadar kullanabílirsiniz", "Uyari!!");
                            txtYatir.Clear();
                        }
                        else
                        {

                            txtArti.Text = creditBalance.ToString();

                            MessageBox.Show("Arti bakiye kullandiniz");
                        }

                       
                    }



                    txtYatir.Clear();
                }
                    
                
            }
        

        }

        private void btnSorgula_Click(object sender, RoutedEventArgs e)
        {

           
           
            double bakiyeniz;
            double miktariniz;

            double credit = 1000;

            double total;
            double creditBalance;
            double borc;



            double.TryParse(txtBakiye.Text, out bakiyeniz);
            double.TryParse(txtYatir.Text, out miktariniz);
            double.TryParse(txtKredi.Text, out borc);
            Atm islem = new Atm(bakiyeniz, miktariniz, credit, borc);



            total = islem.sorgula();
            creditBalance = islem.artiPara();

          
            

            txtBakiye.Text = total.ToString();
            txtArti.Text = creditBalance.ToString();
            int counter = 0;
            counter++;
            if (counter > 0)
            {
                btnSorgula.IsEnabled = false;
            }



        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("EMIN MISINIZ?", "UYARI!!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

                this.Close();
            }
         
           
        }

        private void btnOde_Click(object sender, RoutedEventArgs e)
        {

            double miktariniz;
            double bakiyeniz;
            double ekstra;
            double newEkstra;
            double borc;
            double.TryParse(txtYatir.Text, out miktariniz);
            double.TryParse(txtBakiye.Text, out bakiyeniz);
            double.TryParse(txtArti.Text, out ekstra);
            double.TryParse(txtKredi.Text, out borc);


            Atm islem = new Atm(bakiyeniz, miktariniz, ekstra,borc); ;

            newEkstra = 1000 - ekstra;

            double kredi;

            if (MessageBox.Show(newEkstra+"Tutarinda Odeme Yapilacaktir Onayliyor Musunuz? ", "UYARI!!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (ekstra != 1000)
                {
                    double total = islem.borcOde();
                    txtArti.Text = total.ToString();
                    
                }
                else if(borc < 0 )
                {
                    kredi = islem.krediOde();
                    txtKredi.Text = kredi.ToString();

                }
                else
                {
                    MessageBox.Show("Borcunuz bulunmamaktadir", "Uyari!!");
                }
            }
                
          

            txtYatir.Clear();


        }

      

        private void btnKredi_Click(object sender, RoutedEventArgs e)
        {

            double miktariniz;
            double bakiyeniz;
            double borc;


            double.TryParse(txtYatir.Text, out miktariniz);
            double.TryParse(txtBakiye.Text, out bakiyeniz);
            double.TryParse(txtKredi.Text, out borc);



            Atm islem = new Atm(bakiyeniz, miktariniz,1000,borc);




            double kredi = islem.krediCek();
            double total = islem.paraCek();

            if(bakiyeniz > 1000)
            {
                if(Convert.ToInt32(txtYatir.Text)< 5000)
                {
                    MessageBox.Show("5000 e kadar kredi kullanabilirsiniz");


                    if (MessageBox.Show(txtYatir.Text + " tutarinda kredi cekilecektir onayliyor musunuz?", "Uyari!!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {

                        txtKredi.Text = kredi.ToString();
                        txtYatir.Clear();
                        MessageBox.Show("Kredi Kullandiniz");
                    }

                }
                else
                {
                    MessageBox.Show("Kullanabileceginiz maksimum kredi miktari 5000dir");
                    txtYatir.Clear();
                }
                

                //txtYatir.Text= kredi.ToString();
               
              

               

                

                
            }
            else
            {
                MessageBox.Show("Kredi icin uygun bulunamadiniz daha sonra tekrar deneyiniz.", "Uyari!!");
            }

        }
    }
}
