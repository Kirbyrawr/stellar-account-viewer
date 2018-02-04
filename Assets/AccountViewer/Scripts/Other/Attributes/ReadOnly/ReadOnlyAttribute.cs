using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage (AttributeTargets.Field,Inherited = true)]
public class ReadOnlyAttribute : PropertyAttribute {}