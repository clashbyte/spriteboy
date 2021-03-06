﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SpriteBoy {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ShaderSources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ShaderSources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SpriteBoy.ShaderSources", typeof(ShaderSources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #version 120
        ///
        ///uniform vec4 diffuseColor;
        ///uniform sampler2D texture;
        ///uniform bool discardPass;
        ///uniform float lightMult;
        ///
        ///varying vec3 normal;
        ///varying vec2 texCoord;
        ///
        ///void main() {
        ///	vec4 c = texture2D(texture, texCoord) * diffuseColor;
        ///	c = vec4(mix(c.rgb * dot(normal, vec3(0.0, 1.0, 0.0)), c.rgb, lightMult), c.a);
        ///	if(discardPass){
        ///		if(c.a &lt; 1.0) discard;
        ///	}
        ///	gl_FragColor = c;
        ///}.
        /// </summary>
        internal static string MeshFragment {
            get {
                return ResourceManager.GetString("MeshFragment", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #version 120
        ///
        ///attribute vec3 inPosition;
        ///attribute vec3 inNormal;
        ///attribute vec2 inTexCoord;
        ///
        ///uniform mat4 projectionMatrix;
        ///uniform mat4 cameraMatrix;
        ///uniform mat4 entityMatrix;
        ///uniform mat4 textureMatrix;
        ///
        ///varying vec3 normal;
        ///varying vec2 texCoord;
        ///
        ///void main() {
        ///	mat4 m = projectionMatrix * cameraMatrix * entityMatrix;
        ///	vec4 v = vec4(inPosition, 1.0);
        ///	vec4 n = vec4(inNormal, 0.0);
        ///	texCoord = (textureMatrix * vec4(inTexCoord, 0.0, 0.0)).xy;
        ///	normal = (m * n).xyz;
        ///	gl_Position = m *  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string MeshVertex {
            get {
                return ResourceManager.GetString("MeshVertex", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #version 120
        ///
        ///attribute vec3 inFirstPosition;
        ///attribute vec3 inSecondPosition;
        ///attribute vec3 inFirstNormal;
        ///attribute vec3 inSecondNormal;
        ///attribute vec2 inTexCoord;
        ///
        ///uniform mat4 projectionMatrix;
        ///uniform mat4 cameraMatrix;
        ///uniform mat4 entityMatrix;
        ///uniform mat4 textureMatrix;
        ///uniform float delta;
        ///
        ///varying vec3 normal;
        ///varying vec2 texCoord;
        ///
        ///void main() {
        ///	mat4 m = projectionMatrix * cameraMatrix * entityMatrix;
        ///	vec4 v = vec4(mix(inFirstPosition, inSecondPosition, delta), 1.0);
        ///	vec [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string MorphMeshVertex {
            get {
                return ResourceManager.GetString("MorphMeshVertex", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to .
        /// </summary>
        internal static string SkinnedMeshVertex {
            get {
                return ResourceManager.GetString("SkinnedMeshVertex", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #version 120
        ///
        ///uniform vec4 diffuseColor;
        ///uniform sampler2D texture;
        ///varying vec2 texCoord;
        ///
        ///void main() {
        ///	gl_FragColor = texture2D(texture, texCoord) * diffuseColor;
        ///}.
        /// </summary>
        internal static string SkyFragment {
            get {
                return ResourceManager.GetString("SkyFragment", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #version 120
        ///
        ///attribute vec3 inPosition;
        ///attribute vec2 inTexCoord;
        ///
        ///uniform mat4 projectionMatrix;
        ///uniform mat4 cameraMatrix;
        ///uniform mat4 entityMatrix;
        ///uniform mat4 textureMatrix;
        ///
        ///varying vec2 texCoord;
        ///
        ///void main() {
        ///	mat4 m = projectionMatrix * cameraMatrix * entityMatrix;
        ///	vec4 v = vec4(inPosition, 1.0);
        ///	texCoord = (textureMatrix * vec4(inTexCoord, 0.0, 0.0)).xy;
        ///	gl_Position = m * v;
        ///}.
        /// </summary>
        internal static string SkyVertex {
            get {
                return ResourceManager.GetString("SkyVertex", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #version 120
        ///
        ///uniform vec4 diffuseColor;
        ///
        ///void main() {
        ///	gl_FragColor = diffuseColor;
        ///}.
        /// </summary>
        internal static string WireCubeFragment {
            get {
                return ResourceManager.GetString("WireCubeFragment", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #version 120
        ///
        ///attribute vec3 inPosition;
        ///
        ///uniform mat4 projectionMatrix;
        ///uniform mat4 cameraMatrix;
        ///uniform mat4 entityMatrix;
        ///
        ///void main() {
        ///	mat4 m = projectionMatrix * cameraMatrix * entityMatrix;
        ///	vec4 v = vec4(inPosition, 1.0);
        ///	gl_Position = m * v;
        ///}.
        /// </summary>
        internal static string WireCubeVertex {
            get {
                return ResourceManager.GetString("WireCubeVertex", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #version 120
        ///
        ///varying vec4 color;
        ///
        ///void main() {
        ///	gl_FragColor = color;
        ///}.
        /// </summary>
        internal static string WireGridFragment {
            get {
                return ResourceManager.GetString("WireGridFragment", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #version 120
        ///
        ///attribute vec3 inPosition;
        ///attribute vec4 inColor;
        ///
        ///uniform mat4 projectionMatrix;
        ///uniform mat4 cameraMatrix;
        ///uniform mat4 entityMatrix;
        ///
        ///varying vec4 color;
        ///
        ///void main() {
        ///	color = inColor;
        ///	
        ///	mat4 m = projectionMatrix * cameraMatrix * entityMatrix;
        ///	vec4 v = vec4(inPosition, 1.0);
        ///	gl_Position = m * v;
        ///}.
        /// </summary>
        internal static string WireGridVertex {
            get {
                return ResourceManager.GetString("WireGridVertex", resourceCulture);
            }
        }
    }
}
