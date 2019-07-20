using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibReplanetizer
{
    public interface ICustomGLControl
    {
        Level level { get; set; }

        Matrix4 worldView { get; set; }

        int shaderID { get; set; }
        int colorShaderID { get; set; }
        int collisionShaderID { get; set; }
        int matrixID { get; set; }
        int colorID { get; set; }

    }
}
