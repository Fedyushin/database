using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        SpeechSynthesizer sp1 = new SpeechSynthesizer();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            sp1.Dispose();
            sp1 = new SpeechSynthesizer();
            sp1.SpeakAsync("Welcome. Ask a question.");

            Choices ch = new Choices();
            ch.Add(new string[] { "How are you?", "What is your name?" });
            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(ch);
            Grammar gm1 = new Grammar(gb);
            sre.SetInputToDefaultAudioDevice();
            sre.LoadGrammarAsync(gm1);
            sre.SpeechRecognized += sre_SpeechRecognized;
        }

        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            richTextBox1.Text += e.Result.Text;
            sp1.Dispose();
            sp1 = new SpeechSynthesizer();
            switch (richTextBox1.Text)
            {
                case "How are you?":
                    sp1.SpeakAsync("I am fine. What about you?");
                    richTextBox1.Text = "";
                    break;

                case "What is your name?":
                    sp1.SpeakAsync("My name is PC.");
                    richTextBox1.Text = "";
                    break;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.BackColor == Color.Green)
            {
                button1.BackColor = Color.Red;
                button1.Text = "Stop";
                sre.RecognizeAsync(RecognizeMode.Multiple);
            }
            else
            {
                button1.BackColor = Color.Green;
                button1.Text = "Start";
                sre.RecognizeAsyncStop();
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
