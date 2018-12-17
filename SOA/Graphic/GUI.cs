using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Collections.Generic;

using SOA.Interface;
using SOA.Helper;

namespace SOA.Graphic
{
    public class GUI : IGui, IInit
    {
        private FormHelper m_FormHelper = FormHelper.instance;
        private Color m_BackColor;

        private List<Button> m_Buttons = new List<Button>();

        public void Init()
        {
            m_BackColor = m_FormHelper.BackColor;
        }

        public void SetCursor(Cursor cursor)
        {
            m_FormHelper.Cursor = cursor;
        }

        public void SetOpacity(float opacity)
        {
            m_FormHelper.Opacity = opacity;
        }
        public void SetBackgroundColor(Color color)
        {
            m_FormHelper.BackColor = color;
        }

        public void SetTransparency(bool isTransparency)
        {
            if(isTransparency == true)
            {
                m_BackColor = m_FormHelper.BackColor;

                m_FormHelper.BackColor = Color.Black;
                m_FormHelper.TransparencyKey = Color.Black;

                foreach(Button button in m_Buttons)
                {
                  //  button.BackColor = Color.Black;
                }
            }
            else
            {
                SetBackgroundColor(m_BackColor);
                m_FormHelper.TransparencyKey = m_BackColor == Color.Black ? Color.White : Color.Black;
            }
        }

        public Button AddButton(string text, int x, int y, int width, int height)
        {
            Button button = new Button();

            button.BackColor = Color.White;
            button.ForeColor = Color.Black;

            button.Text     = text;
            button.Location = new Point(x, y);
            button.Width    = width;
            button.Height   = height;

            m_Buttons.Add(button);

            m_FormHelper.Controls.Add(button);

            return button;
        }
    }
}
