﻿using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Intersect.Editor.Classes.Core;
using Intersect.GameObjects.Events;
using Intersect.Editor.Classes.Localization;

namespace Intersect.Editor.Forms.Editors.Event_Commands
{
    public partial class EventCommandChangeFace : UserControl
    {
        private readonly FrmEvent mEventEditor;
        private EventCommand mMyCommand;

        public EventCommandChangeFace(EventCommand refCommand, FrmEvent editor)
        {
            InitializeComponent();
            mMyCommand = refCommand;
            mEventEditor = editor;
            cmbFace.Items.Clear();
            cmbFace.Items.AddRange(GameContentManager.GetSmartSortedTextureNames(GameContentManager.TextureType.Face));
            if (cmbFace.Items.IndexOf(mMyCommand.Strs[0]) > -1)
            {
                cmbFace.SelectedIndex = cmbFace.Items.IndexOf(mMyCommand.Strs[0]);
            }
            else
            {
                cmbFace.SelectedIndex = 0;
            }
            UpdatePreview();
            InitLocalization();
        }

        private void InitLocalization()
        {
            grpChangeFace.Text = Strings.EventChangeFace.title;
            lblFace.Text = Strings.EventChangeFace.label;
            btnSave.Text = Strings.EventChangeFace.okay;
            btnCancel.Text = Strings.EventChangeFace.cancel;
        }

        private void UpdatePreview()
        {
            Bitmap destBitmap = new Bitmap(pnlPreview.Width, pnlPreview.Height);
            Graphics g = Graphics.FromImage(destBitmap);
            g.Clear(System.Drawing.Color.Black);
            if (File.Exists("resources/faces/" + cmbFace.Text))
            {
                Bitmap sourceBitmap = new Bitmap("resources/faces/" + cmbFace.Text);
                g.DrawImage(sourceBitmap,
                    new Rectangle(pnlPreview.Width / 2 - sourceBitmap.Width / 2,
                        pnlPreview.Height / 2 - sourceBitmap.Height / 2, sourceBitmap.Width, sourceBitmap.Height),
                    new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height), GraphicsUnit.Pixel);
            }
            g.Dispose();
            pnlPreview.BackgroundImage = destBitmap;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mMyCommand.Strs[0] = cmbFace.Text;
            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }

        private void cmbSprite_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }
    }
}