using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace DroneLander.MissionControl
{
    public static class ControlExtensions
    {
        public static IEnumerable<T> GetChildrenOfType<T>(this DependencyObject start) where T : class
        {
            var queue = new Queue<DependencyObject>(); queue.Enqueue(start); while (queue.Count > 0)
            {
                var item = queue.Dequeue(); var realItem = item as T; if (realItem != null) { yield return realItem; }
                int count = VisualTreeHelper.GetChildrenCount(item); for (int i = 0; i < count; i++) { queue.Enqueue(VisualTreeHelper.GetChild(item, i)); }
            }
        }
    }
}
