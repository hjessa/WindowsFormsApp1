using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        bool turn = true; //wartość true = tura X, wartość false = tura O
        int turn_count = 0; //liczebność tur
        public Form1()
        {
            InitializeComponent();
        }
        //okienko z tekstem wyświetlane po kliknięciu przycisku Pomoc
        private void informacjęOGrzeToolStripMenuItem_Click(object sender, EventArgs e) 
        {
            MessageBox.Show("Hubert Jessa i Bartosz Dereziński");
        }
        //wyłaczeniem aplikacji po kliknięciu przycisku Wyjśćie
        private void wyjścieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //Metoda decydująca o pojawieniu się odpowiedniego znaku w zależności od kolejki 
        //pobranie nazwy przycisku za pomocą sendera do zmiennej b klasy Button
        //zaczynamy od X i instrukcja pierwsza go pokaze nastepnie zmienna turn po turze przyjmuje wartosc false i wartosc naszego b zmienia sie na O i tak w kolko
        //następnie kliknięty przycisk zostaje zablokowany do ponownego użytku a liczebność kliknięcia wzrasta o 1
        private void button_click(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            if (turn)
                b.Text = "X";
            else
                b.Text = "O";
            turn = !turn;
            b.Enabled = false;
            turn_count++;
            Checkforwinner();
            
        }
        //instrukcja sprawdzająca czy wartości klikniętych przycisków dają wygraną lub remis
        private void Checkforwinner()
        {
            bool winner = false; //zmienna o wartosci true oznacza zwyciestwo


            //sprawdzanie pionowe
            if ((A1.Text == B1.Text) && (B1.Text == C1.Text) && (!A1.Enabled))
                winner = true;
            else if ((A2.Text == B2.Text) && (B2.Text == C2.Text) && (!A2.Enabled))
                winner = true;
            else if ((A3.Text == B3.Text) && (B3.Text == C3.Text) && (!A3.Enabled))
                winner = true;

            //sprawdzanie poziome
            if ((A1.Text == A2.Text) && (A2.Text == A3.Text) && (!A1.Enabled))
                winner = true;
            else if ((B1.Text == B2.Text) && (B2.Text == B3.Text) && (!B1.Enabled))
                winner = true;
            else if ((C1.Text == C2.Text) && (C2.Text == C3.Text) && (!C1.Enabled))
                winner = true;

            //sprawdzanie po skosie
            if ((A1.Text == B2.Text) && (B2.Text == C3.Text) && (!A1.Enabled))
                winner = true;
            else if ((A3.Text == B2.Text) && (B2.Text == C1.Text) && (!C1.Enabled))
                winner = true;




            if (winner)
            {
                disableButtons();
                string congratulations = ""; //zmienna z symbolem zwycięskiego gracza
                if (turn)
                {
                    congratulations = "O";
                    ocount.Text = (Int32.Parse(ocount.Text) + 1).ToString(); //inkrementacja sumy zwycięstw dla O
                }
                else
                {
                    congratulations = "X";
                    xcount.Text = (Int32.Parse(xcount.Text) + 1).ToString(); //inkrementacja sumy zwycięstw dla X
                }

                MessageBox.Show("Wygrywa gracz: " + congratulations);
            }
            else
            {
                if(turn_count==9)
                {
                    drawcount.Text = (Int32.Parse(drawcount.Text) + 1).ToString(); //inkrementacja sumy remisów, przez nacisnięcie wszystkich przycisków bez zwycięstwa
                    MessageBox.Show("Remis");

                }
            }
        }
        //funkcja wyłączająca przyciski
        //try i catch spowodowane błedem pobierania kontrolek takich jak nasze menu
        private void disableButtons()
        {


            
                foreach (Control c in Controls) //pętla przez wszystkie kontrolki (buttony)
            {
                try
                {
                    Button b = (Button)c; //pobranie nazwy kontrolki i przypisanie do zmiennej b
                    b.Enabled = false; //wyłączenie aktywności przycisku
                }
                catch { }
                }


        }
        //metoda wyzerowania planszy po kliknięciu przycisku Nowa gra
        //try i catch spowodowane błedem pobierania kontrolek takich jak nasze menu
        private void nowaGraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            turn = true;
            turn_count = 0;

            foreach (Control c in Controls) //pętla przez wszystkie kontrolki (buttony)
            {
                try
                {

                    Button b = (Button)c;//pobranie nazwy kontrolki i przypisanie do zmiennej b
                    b.Enabled = true; //włączenie przycisków
                    b.Text = ""; //wyzerowanie tekstu (X i O)
                }
                catch { }
            }
            
        }
        //metoda podświetlająca symbol gracza  na przycisku (którego jest kolejka)
        //przesłanie nazwy obiektu do zmiennej b, następnie instrukcja
        //jeżeli przycisk jest aktywny to: w zależnośco kogo tura (zmienna turn) podświetl X, podświetl Y
        private void button_enter(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Enabled)
            {
                if (turn)
                    b.Text = "X";
                else
                    b.Text = "O";
            }
        }

        //metoda wymazująca button_enter po opuszczeniu przycisku
        private void button_leave(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if(b.Enabled)
            {
                b.Text = "";
            }
        }
        //metoda zerująca liczniki zwycięstw
        private void resetWynikówToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ocount.Text = "0";
            xcount.Text = "0";
            drawcount.Text = "0";
        }
    }
}