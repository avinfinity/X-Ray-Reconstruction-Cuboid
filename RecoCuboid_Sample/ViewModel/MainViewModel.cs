using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RecoCuboid_Sample
{
    internal class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            var allViewInstances =  new List<BaseViewModel>();
            allViewInstances.Add(this);
            var types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => type.IsSubclassOf(typeof(BaseViewModel)) && !type.IsAbstract && type != this.GetType());
            foreach(var type in types)
            {
                allViewInstances.Add((BaseViewModel)Activator.CreateInstance(type));
            }
            Views = allViewInstances;
        }

        public override string ToString()
        {
            return "Rendering Basics";
        }

        public IEnumerable<BaseViewModel> Views { get; }
    }
}