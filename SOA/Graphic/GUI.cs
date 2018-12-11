using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

using SOA.Interface;
using SOA.Helper;

namespace SOA.Graphic
{
    public class GUI : IGui, IInit
    {
        private FormHelper m_FormHelper = FormHelper.instance;
        private Color m_BackColor;

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
            }
            else
            {
                SetBackgroundColor(m_BackColor);
                m_FormHelper.TransparencyKey = m_BackColor == Color.Black ? Color.White : Color.Black;
            }
        }

        public void MsgBox(string text, string caption)
        {
            m_FormHelper.MsgBox(text, caption);
        }
    }
}
