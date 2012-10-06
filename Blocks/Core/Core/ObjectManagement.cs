using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core {
    public class ObjectsPool<T> {
        public static void Flush(List<T> deadList) {
            foreach (T p in deadList) {
                pool.Push(p);
            }
            deadList.Clear();
        }

        public static void Flush(T deadObject) {
            pool.Push(deadObject);
        }

        public static void Create(out T obj) {
            if (pool.Count > 0)
                obj = pool.Pop();
            else
                obj = default(T);
        }

        public static T Create() {
            if (pool.Count > 0)
                return pool.Pop();
            else
                return default(T);
        }

        private static Stack<T> pool = new Stack<T>();
    }

    public class ObjectsManager<T> where T : MortalObject, IUpdate, IDraw {
        List<T> objects = new List<T>();
        List<T> deadObjects = new List<T>();
        public void Update(int dt) {
            foreach (T obj in objects) {
                obj.Update(dt);
                if (obj.Dead())
                    deadObjects.Add(obj);
            }
            if (deadObjects.Count > 0) {
                foreach (T obj in deadObjects)
                    objects.Remove(obj);
                ObjectsPool<T>.Flush(deadObjects);
            }
        }

        public void Create(out T obj) {
            ObjectsPool<T>.Create(out obj);
            if (obj != null)
                objects.Add(obj);
        }

        public void Add(T obj) {
            objects.Add(obj);
        }

        public void Draw(int dt) {
            foreach (T obj in objects) {
                obj.Draw(dt);
            }
        }
    }

    public interface MortalObject {
        bool Dead();
    }
}
