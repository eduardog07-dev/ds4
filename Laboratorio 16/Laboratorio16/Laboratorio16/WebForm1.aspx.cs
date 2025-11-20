using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Laboratorio16
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        static float a, c, d;
        static char b;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void b1_click(object sender, EventArgs e)
        {
            if (((tResultado.Text == "+") || (tResultado.Text == "-") || (tResultado.Text == "*") || (tResultado.Text == "/")))
            {
                tResultado.Text = "";
                tResultado.Text = tResultado.Text + b1.Text;
            }
            else
            {
                tResultado.Text = tResultado.Text + b1.Text;
            }
        }

        protected void b2_click(object sender, EventArgs e)
        {
            if (((tResultado.Text == "+") || (tResultado.Text == "-") || (tResultado.Text == "*") || (tResultado.Text == "/")))
            {
                tResultado.Text = "";
                tResultado.Text = tResultado.Text + b2.Text;
            }
            else
            {
                tResultado.Text = tResultado.Text + b2.Text;
            }
        }

        protected void b3_click(object sender, EventArgs e)
        {
            if (((tResultado.Text == "+") || (tResultado.Text == "-") || (tResultado.Text == "*") || (tResultado.Text == "/")))
            {
                tResultado.Text = "";
                tResultado.Text = tResultado.Text + b3.Text;
            }
            else
            {
                tResultado.Text = tResultado.Text + b3.Text;
            }
        }

        protected void b4_click(object sender, EventArgs e)
        {
            if (((tResultado.Text == "+") || (tResultado.Text == "-") || (tResultado.Text == "*") || (tResultado.Text == "/")))
            {
                tResultado.Text = "";
                tResultado.Text = tResultado.Text + b4.Text;
            }
            else
            {
                tResultado.Text = tResultado.Text + b4.Text;
            }
        }

        protected void b5_click(object sender, EventArgs e)
        {
            if (((tResultado.Text == "+") || (tResultado.Text == "-") || (tResultado.Text == "*") || (tResultado.Text == "/")))
            {
                tResultado.Text = "";
                tResultado.Text = tResultado.Text + b5.Text;
            }
            else
            {
                tResultado.Text = tResultado.Text + b5.Text;
            }
        }
        protected void b6_click(object sender, EventArgs e)
        {
            if (((tResultado.Text == "+") || (tResultado.Text == "-") || (tResultado.Text == "*") || (tResultado.Text == "/")))
            {
                tResultado.Text = "";
                tResultado.Text = tResultado.Text + b6.Text;
            }
            else
            {
                tResultado.Text = tResultado.Text + b6.Text;
            }
        }

        protected void b7_click(object sender, EventArgs e)
        {
            if (((tResultado.Text == "+") || (tResultado.Text == "-") || (tResultado.Text == "*") || (tResultado.Text == "/")))
            {
                tResultado.Text = "";
                tResultado.Text = tResultado.Text + b7.Text;
            }
            else
            {
                tResultado.Text = tResultado.Text + b7.Text;
            }
        }

        protected void b8_click(object sender, EventArgs e)
        {
            if (((tResultado.Text == "+") || (tResultado.Text == "-") || (tResultado.Text == "*") || (tResultado.Text == "/")))
            {
                tResultado.Text = "";
                tResultado.Text = tResultado.Text + b8.Text;
            }
            else
            {
                tResultado.Text = tResultado.Text + b8.Text;
            }
        }

        protected void b9_click(object sender, EventArgs e)
        {
            if (((tResultado.Text == "+") || (tResultado.Text == "-") || (tResultado.Text == "*") || (tResultado.Text == "/")))
            {
                tResultado.Text = "";
                tResultado.Text = tResultado.Text + b9.Text;
            }
            else
            {
                tResultado.Text = tResultado.Text + b9.Text;
            }
        }

        protected void b0_click(object sender, EventArgs e)
        {
            if (((tResultado.Text == "+") || (tResultado.Text == "-") || (tResultado.Text == "*") || (tResultado.Text == "/")))
            {
                tResultado.Text = "";
                tResultado.Text = tResultado.Text + b0.Text;
            }
            else
            {
                tResultado.Text = tResultado.Text + b0.Text;
            }
        }

        protected void add_click(object sender, EventArgs e)
        {
            a = Convert.ToInt32(tResultado.Text);
            tResultado.Text = "+";
            b = '+';
        }

        protected void sub_click(object sender, EventArgs e)
        {
            a = Convert.ToInt32(tResultado.Text);
            tResultado.Text = "-";
            b = '-';
        }

        protected void mul_click(object sender, EventArgs e)
        {
            a = Convert.ToInt32(tResultado.Text);
            tResultado.Text = "*";
            b = '*';
        }
        protected void div_click(object sender, EventArgs e)
        {
            a = Convert.ToInt32(tResultado.Text);
            tResultado.Text = "/";
            b = '/';
        }

        protected void eql_click(object sender, EventArgs e)
        {
            c = Convert.ToInt32(tResultado.Text);
            tResultado.Text = "";
            if (b == '/')
            {
                d = a / c;
                tResultado.Text += d;
                a = d;
            }
            else if (b == '+')
            {
                d = a + c;
                tResultado.Text += d;
                a = d;
            }
            else if (b == '-')
            {
                d = a - c;
                tResultado.Text += d;
                a = d;
            }
            else // Asume multiplicación (*)
            {
                d = a * c;
                tResultado.Text += d;
                a = d;
            }
        }

        protected void clr_click(object sender, EventArgs e)
        {
            tResultado.Text = "";
        }
    }
}