using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum COMPONENT
{
    Resistor,
    Transformer,
    Capacitor,
    Diode
}

public interface ComponentPuzzlePart
{
    public COMPONENT getComponentType(); // Get the type of the component
    public double getValue(); // Get the numerical value of the signal
    public void setValue(); // Set the numerical value of the signal
    public int getPkgType(); // This is the package type of the component i.e how it fits on the board
}
