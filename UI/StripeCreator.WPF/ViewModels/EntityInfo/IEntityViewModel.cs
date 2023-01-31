using System.Collections.Generic;

namespace StripeCreator.WPF
{
    public interface IEntityViewModel
    {
        public EntityInfoViewModel GetData { get; }
    }
}
