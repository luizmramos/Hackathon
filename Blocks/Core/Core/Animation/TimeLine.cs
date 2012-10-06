using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;


namespace Core {
    public class TimeLineStates : IUpdate {

        int[] time;
        int[] endTime;
        bool[] pauseAtReachEnd;

        public bool playing;

        int index;
        public int State { get { return index; } }

        public float GetFraction() {
            return ((float)CurrentTime) / CurrentEndTime;
        }

        public int StateCount() {
            return time.Count();
        }

        public TimeLineStates(int n) {
            time = new int[n];
            endTime = new int[n];
            pauseAtReachEnd = new bool[n];
        }

        public void Play() {
            playing = true;
        }

        public void Play(int index) {
            this.index = index;
            playing = true;
            time[index] = 0;
        }

        public void Pause() {
            playing = false;
        }

        public void Pause(int index) {
            this.index = index;
            playing = false;
            time[index] = 0;
        }

        public void Set(int index, int time, bool pauseAtReachEnd) {
            this.endTime[index] = time;
            this.pauseAtReachEnd[index] = pauseAtReachEnd;
        }

        public int CurrentTime { get { return time[index]; } }
        public int CurrentEndTime { get { return endTime[index]; } }


        public void Update(int dt) {
            if (playing) {
                time[index] += dt;

                if (time[index] > endTime[index]) {
                    int nextIndex = index + 1;
                    if (nextIndex >= time.Length) {
                        nextIndex = 0;
                    }

                    if (pauseAtReachEnd[index]) {
                        playing = false;
                        time[nextIndex] = 0;
                    } else {
                        time[nextIndex] = time[index] - endTime[index];
                    }

                    ReachEndOfStateEvent.Fire(index);
                    index = nextIndex;
                }
            }

        }

        public EventOneArg<int> ReachEndOfStateEvent = new EventOneArg<int>();
    }
}
