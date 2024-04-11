﻿using System.Threading;
using System.Collections.Generic;
using Verse;
using System.Linq;
using static Shared.CommonEnumerators;
using System;

namespace GameClient
{
    public static class DialogManager
    {
        //      inputCache
        // Any time a dialog that has inputs is left (it is popped from the stack or a new dialog is pushed)
        // ,it will save its own list of inputs to inputCache
        // inputs can also be manually set to save.
        public static List<object> inputCache;

        //      inputReserve
        //  Unlike inputCache, inputReserve never automatically gets updated, and must be set using
        //  the
        public static List<object> inputReserve;

        //an internal stack to keep track of windows
        //(this makes it easier and more accurate to find the last window pushed)
        private static Stack<Window> windowStack = new Stack<Window>();

        public static RT_WindowInputs currentDialogInputs;

        public static void PushNewDialog(Window window)
        {
            if (ClientValues.isReadyToPlay || Current.ProgramState == ProgramState.Entry)
            {
                try
                {
                    //Hide the current window
                    if (windowStack.Count > 0)
                        Find.WindowStack.TryRemove(windowStack.Peek());

                    //add the new window to the internal stack
                    windowStack.Push(window);

                    //Get an instance of the new window as RT_WindowInputs so input info can be retrieved later
                    if (window is RT_WindowInputs) currentDialogInputs = (RT_WindowInputs)window;


                    //draw the new window
                    Find.WindowStack.Add(window);
                }
                catch (Exception e) { Logger.WriteToConsole(e.ToString(), LogMode.Error); }
            }
        }

        public static void PopInternalStack()
        {
            if (windowStack.Count > 0) windowStack.Pop();
        }

        public static void clearInternalStack()
        {
            windowStack.Clear();
        }

        public static void clearStack()
        {
            while (windowStack.Count > 0)
            {
                Find.WindowStack.TryRemove(windowStack.Pop(), true);
                if (windowStack.Count > 0)
                    Find.WindowStack.Add(windowStack.Peek());
            }
        }

        public static void PopDialog() {

            if (windowStack.Count > 0)
            {
                Find.WindowStack.TryRemove(windowStack.Pop(), true);
                if (windowStack.Count > 0) Find.WindowStack.Add(windowStack.Peek());
            }
        }

        public static void PopDialog(Type type)
        {
            Stack<Window> TempStack = new Stack<Window>();
            //find the window to remove
            while (windowStack.Count() != 0)
            {
                if (windowStack.Peek().GetType() == type)
                {
                    bool CurrentlyDrawn = Find.WindowStack.TryRemove(type, true);
                    windowStack.Pop();
                    if ((windowStack.Count > 0) && CurrentlyDrawn) Find.WindowStack.Add(windowStack.Peek());
                    continue;
                }
                TempStack.Push(windowStack.Pop());
            }

            //Make 100% the stack is empty
            windowStack.Clear();

            //Put the items back into WindowStack
            while (TempStack.Count() != 0)
            {
                windowStack.Push(TempStack.Pop());
            }
        }

        public static void SetInputReserve()
        {
            currentDialogInputs.CacheInputs();
            inputReserve = new List<object>(inputCache);
        }

    }
}
