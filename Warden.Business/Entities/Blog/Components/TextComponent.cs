using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Business.Entities.Blog.Components
{
    public class TextComponent : Component
    {
        public override string Name => "Text";

        public override string Content { get; set; }
    }
}
