using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;

namespace Blocks
{
    class PageLevel : UIRoot
    {

        UIButton btnFacil;

        UIButton btnInterm;
        
        UIButton btnDificil;
        
        UILabel lblFacil;
        
        UILabel lblInterm;
        
        UILabel lblDificil;

        UILabel lblTitulo;

        float altura, largura, xF, yF, xI, yI, xD, yD, xT,yT;

        public PageLevel()
        {

            Add(btnFacil = new UIButton(Tex.btnSimple, Tex.btnSimple_pressed),
                btnDificil=new UIButton(Tex.btnSimple,Tex.btnSimple_pressed),
                btnInterm=new UIButton(Tex.btnSimple,Tex.btnSimple_pressed),
                lblDificil=new UILabel(Font.andy30).SetText("Hard"),
                lblFacil = new UILabel(Font.andy30).SetText("Easy"),
                lblInterm = new UILabel(Font.andy30).SetText("Medium"),
                lblTitulo = new UILabel(Font.andy30).SetText("Choose your level")
            );
            xD = 0.5f;
            xF = 0.5f;
            xI = 0.5f;
            yF = 0.416f;
            yI = 0.833f;
            yD = 1.245f;
            xT = 0.5f;
            yT = 0.2f;

            altura = 0.2f;
            largura = 0.7f;

            btnFacil.SetWidth(largura).SetHeight(altura).SetY(yF).SetX(xF);
            btnInterm.SetWidth(largura).SetHeight(altura).SetY(yI).SetX(xI);
            btnDificil.SetWidth(largura).SetHeight(altura).SetY(yD).SetX(xD);
            lblDificil.SetAlignment(1).SetX(xD).SetY(yD);
            lblInterm.SetAlignment(1).SetX(xI).SetY(yI);
            lblFacil.SetAlignment(1).SetX(xF).SetY(yF);
            lblTitulo.SetAlignment(1).SetX(xT).SetY(yT);

            //Add(btnPlay = new UIButton(Tex.btnSimple, Tex.btnSimple_pressed),
            //    lblHello = new UILabel(Font.andy30).SetText("Hello"),
            //    imgStone = new UISprite(Tex.stone),
            //    new UIContainer().Add(
            //        btnNext = new UIButton(Tex.btnSimple, Tex.btnSimple_pressed),
            //        new UILabel(Font.andy30).SetText("Next").SetAlignment(1).SetDepth(0.1f)
            //    ).SetX(0.7f).SetY(1.2f)
            //);

            //btnPlay.SetWidth(0.0208f).SetHeight(0.0208f).SetY(0.5f).SetX(0.5f);
            //lblHello.SetAlignment(1).SetX(0.5f).SetY(0.2f);
            //imgStone.SetWidth(0.2f).SetHeight(0.2f).SetX(0.5f).SetY(0.8f);

            //btnNext.SetWidth(0.0208f).SetHeight(0.0208f);

            CreateAnimations();
            CreateHandlers();
        }

        void CreateAnimations()
        {
            //affStoneRotation = new AffectorF1(imgStone.Rotation, 0, (float)Math.PI * 2, 1000);
        }

        void CreateHandlers()
        {
            btnFacil.OnRelease.Add((ti) =>
            {
                MyUiManager.Get().SwitchtoFases(0);
            });

            btnInterm.OnRelease.Add((ti) =>
            {
                MyUiManager.Get().SwitchtoFases(1);
            });
            
            btnDificil.OnRelease.Add((ti) => {
                MyUiManager.Get().SwitchtoFases(2);
            });
        }

        public override void Update(int dt)
        {
            //affStoneRotation.Update(dt);
            base.Update(dt);
        }

    }
}