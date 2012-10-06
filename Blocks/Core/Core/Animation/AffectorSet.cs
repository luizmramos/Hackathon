using System;
using System.Collections.Generic;
using System.Linq;

namespace Core {
    public class AffectorSet : AffectorFinite {

        Dictionary<AffectorFinite, int> affectors = new Dictionary<AffectorFinite, int>();

        public AffectorSet Add(AffectorFinite aff, int startTime) {
            affectors.Add(aff, startTime);
            aff.End.Add(aff_End);
            return this;
        }

        void aff_End() {
            endCount--;
        }

        int endCount;
        public override void Begin() {
            if (!Active) {
                endCount = affectors.Count;

                base.Begin();
            }
        }

        public override AffectorFinite Stop() {
            foreach (AffectorFinite aff in affectors.Keys) {
                aff.Active = false;
            }
            return base.Stop();
        }

        public override void Update(int dt) {
            if (!Active) return;

            float lastTime = currentTime;
            currentTime += dt;

            foreach (AffectorFinite aff in affectors.Keys) {
                int time = affectors[aff];
                if (time >= lastTime && time < currentTime) {
                    aff.Begin();
                }
                aff.Update(dt);
            }
            if (endCount <= 0) {
                Active = false;
                End.Fire();
            }
        }

        public override void SetEndParameters() {
            List<KeyValuePair<AffectorFinite, int>> myList = affectors.ToList();

            myList.Sort(
                delegate(KeyValuePair<AffectorFinite, int> firstPair,
                KeyValuePair<AffectorFinite, int> nextPair) {
                    return firstPair.Value.CompareTo(nextPair.Value);
                }
            );

            foreach (KeyValuePair<AffectorFinite, int> pair in myList) {
                pair.Key.SetEndParameters();
            }
        }
    }

    public class AffectorChain : AffectorFinite {

        List<AffectorFinite> affectors = new List<AffectorFinite>();

        private AffectorFinite currentAffector;
        private int currentAffectorIndex;

        public AffectorChain Add(AffectorFinite aff) {
            affectors.Add(aff);
            aff.End.Add(aff_End);
            return this;
        }

        public AffectorChain Wait(float deltaT) {
            Add(new AffectorWait(deltaT));
            return this;
        }

        void aff_End() {
            currentAffectorIndex++;
            if (currentAffectorIndex >= affectors.Count) {
                Active = false;
                FireEnd();
                return;
            }

            // TODO Affectors com duracao muito curta precisam ser tratados devidamente
            currentAffector = affectors[currentAffectorIndex];
            currentAffector.Begin();
        }

        public override void Begin() {
            if (!Active) {
                currentAffector = affectors[0];
                currentAffectorIndex = 0;
                currentAffector.Begin();
                base.Begin();
            }
        }

        public override AffectorFinite Stop() {
            foreach (AffectorFinite aff in affectors) {
                aff.Active = false;
            }
            return base.Stop();
        }

        public override void Update(int dt) {
            if (!Active) return;
            currentAffector.Update(dt);
        }

        public override void SetEndParameters() {
            foreach (AffectorFinite aff in affectors) {
                aff.SetEndParameters();
            }
        }
    }
}
