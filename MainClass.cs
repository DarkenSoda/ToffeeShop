/*
    CS251
    Assignment 3 - ToffeShop Implementation
    Authors:
        Mahmoud Adel Ahmed
        Nour El-Deen Ahmed
        Yassin Tarek Helmy

    Note: Email Authentication doesn't work on some devices, but works on others.
    We don't know the reason so we added a way to bypass authentication if needed.
*/


using System;

/* This is the main entry point of the C# program. It creates an instance of the `SystemManager` class
and calls its `SystemRun()` method to start the program. The `namespace` keyword is used to define a
scope that contains a set of related objects, in this case, the `MainClass` class. */
namespace CS251_A3_ToffeeShop {
    public class MainClass {
        public static void Main(string[] args) {
            SystemManager systemManager = new SystemManager();
            systemManager.SystemRun();
        }
    }
}