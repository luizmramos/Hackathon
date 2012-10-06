using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;

namespace Blocks
{
    class PageMenu : UIRoot
    {
        UIButton btnPlay, btnInst;
        UILabel lblName, lblPlay, lblInst;
        UISprite figMenu;

        float btnWidth = 0.7f;
        float btnHeigth = 0.2f;
        float btnPlayY = 1.2f;
        float btnInstY = 1.4f;

        float figWidth = 0.6f;
        float figHeigth = 0.6f;
        float figY = 0.6f;

        float lblWidth = 0.4f;
        float lblHeigth = 0.1f;
        float nameY = 0.2f;

        float center = 0.5f;


        public PageMenu()
        {

            Add(figMenu = new UISprite(Tex.stone),
                btnPlay = new UIButton(Tex.btnSimple, Tex.btnSimple_pressed),
                btnInst = new UIButton(Tex.btnSimple, Tex.btnSimple_pressed),
                lblPlay = new UILabel(Font.andy30),
                lblInst = new UILabel(Font.andy30),
                lblName = new UILabel(Font.andy30));

            btnPlay.SetWidth(btnWidth).SetHeight(btnHeigth).SetY(btnPlayY).SetX(center);
            btnInst.SetWidth(btnWidth).SetHeight(btnHeigth).SetY(btnInstY).SetX(center);
            lblPlay.SetText("Play").SetAlignment(1).SetWidth(lblWidth).SetHeight(0.052f).SetY(btnPlayY).SetX(center);
            lblInst.SetText("Instructions").SetAlignment(1).SetWidth(lblWidth).SetHeight(0.052f).SetY(btnInstY).SetX(center);
            figMenu.SetWidth(figWidth).SetHeight(figHeigth).SetY(figY).SetX(center);
            lblName.SetText("Blocks!").SetAlignment(1).SetWidth(lblWidth).SetHeight(lblHeigth).SetY(nameY).SetX(center);

            CreateHandlers();
        }

        void CreateHandlers()
        {

            btnPlay.OnRelease.Add((ti) =>
            {
                MyUiManager.Get().SwitchtoLevel();
            });

            btnInst.OnRelease.Add((ti) =>
            {
                MyUiManager.Get().SwitchtoInst1();
            });

        }

        public override void Update(int dt)
        {
            base.Update(dt);
        }

    }
}