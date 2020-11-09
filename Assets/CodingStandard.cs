using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


namespace NameSpace // Namespaces are PascalCase
{
    // Interfaces and Classes are PascalCase
    public interface ICodingStandard // Interface name must start with I-
    {
        //...
    }

    class ClassName : MonoBehaviour, ICodingStandard 
    {
        private int variableName; // non-static fields would be camelCase

        public static int StaticVariableName; // static fields should be in PascalCase

        private const int CONSTANT_VALUE = 100; // constants should be in SNAKE_CASE

        private bool condition = true;

        // Prefer explicit use of SerializeField for variables that need to be in the editor
        [SerializeField] int exposedInEditor; // Keep Ser

        // Method names should be PascalCase
        private void DoSomething(int statusCode) // Method inputs should be in camelCase
        {
            // Conditional statements should always have braces
            if (condition)
            {
                DoSomething(1); 
            }
        }
    }
}