using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;

namespace Blocks
{
    class PageLevel : UIRoot
    {

        UIButton btnPlay;

        UILabel lblHello;

        UISprite imgStone;

        UIButton btnNext;

        AffectorF1 affStoneRotation;

        public PageLevel()
        {

            Add(imgStone = new UISprite(Tex.stone),
                btnPlay = new UIButton(Tex.btnSimple, Tex.btnSimple_pressed));

            btnPlay.SetWidth(0.052f).SetHeight(0.052f).SetY(0.5f).SetX(0.5f);

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
            affStoneRotation = new AffectorF1(imgStone.Rotation, 0, (float)Math.PI * 2, 1000);
        }

        void CreateHandlers()
        {
            //btnPlay.OnRelease.Add((ti) => {
            //    affStoneRotation.Begin();
            //});

            //btnNext.OnRelease.Add((ti) => {
            //    MyUiManager.Get().SwitchToPage2();
            //});
        }

        public override void Update(int dt)
        {
            affStoneRotation.Update(dt);
            base.Update(dt);
        }

    }
}