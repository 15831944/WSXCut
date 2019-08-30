using System;

namespace WSX.CommomModel.Physics
{
    public class UnitObserver<T>
    { 
        public T UnitType { get; set; }

        public event Action<T> UnitChanged;

        public void ChangeUnit(T t)
        {
            UnitType = t;
            UnitChanged?.Invoke(t);
        }    
    }
}
