using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;

namespace Blocks
{
    class PageFases : UIRoot
    {

        UILabel lblTitulo;
        static int qtdFases=10;
        UIButton[] vButton=new UIButton[qtdFases];
        float[] posicX = new float[qtdFases];
        float[] posicY = new float[qtdFases];
        UILabel[] idFase = new UILabel[qtdFases];
        float altura, largura,razao,xInic,yInic;
        int qtdLinha=5;
        int level;

        public PageFases(int level)
        {
            this.level = level;
            altura = 0.1f;
            largura = 0.1f;
            xInic = 0.1f;
            yInic = 0.4f;
            posicX[0] = xInic;
            posicY[0] = yInic;
            razao = 0.2f;

            for (int i = 1; i < qtdFases; i++) {
                posicX[i] = posicX[0] + razao*(i%qtdLinha);
                posicY[i] = posicY[0] + razao *((float) (i / qtdLinha));
            }

            for (int i = 0; i < qtdFases; i++) {
                Add(vButton[i] = new UIButton(Tex.btnSimple, Tex.btnSimple_pressed),
                    idFase[i]=new UILabel(Font.andy30).SetText(i+1+"")
                    );
                vButton[i].SetHeight(altura).SetWidth(largura).SetX(posicX[i]).SetY(posicY[i]);
                idFase[i].SetAlignment(1).SetX(posicX[i]).SetY(posicY[i]);
            }
           

            
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
            for (int i = 0; i < qtdFases; i++)
            {
                vButton[i].OnRelease.Add((ti) =>
                {
                    MyUiManager.Get().SwitchtoGame(level,i);
                });

            }
        }

        public override void Update(int dt)
        {
            //affStoneRotation.Update(dt);
            base.Update(dt);
        }

    }
}