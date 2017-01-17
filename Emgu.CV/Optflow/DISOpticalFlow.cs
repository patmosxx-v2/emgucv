﻿//----------------------------------------------------------------------------
//  Copyright (C) 2004-2017 by EMGU Corporation. All rights reserved.       
//----------------------------------------------------------------------------

using System;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using Emgu.Util;
using Emgu.CV.Structure;
using System.Runtime.InteropServices;

namespace Emgu.CV
{
    /// <summary>
    /// DIS optical flow algorithm.
    /// This class implements the Dense Inverse Search(DIS) optical flow algorithm.Includes three presets with preselected parameters to provide reasonable trade-off between speed and quality.However, even the slowest preset is still relatively fast, use DeepFlow if you need better quality and don't care about speed.
    /// More details about the algorithm can be found at:
    /// Till Kroeger, Radu Timofte, Dengxin Dai, and Luc Van Gool. Fast optical flow using dense inverse search. In Proceedings of the European Conference on Computer Vision (ECCV), 2016.
    /// </summary>
    public partial class DISOpticalFlow : UnmanagedObject, IDenseOpticalFlow
    {
        /// <summary>
        /// Preset
        /// </summary>
        public enum Preset
        {
            /// <summary>
            /// Ultra fast
            /// </summary>
            UltraFast = 0,
            /// <summary>
            /// Fast
            /// </summary>
            Fast = 1, 
            /// <summary>
            /// Medium
            /// </summary>
            Medium = 2
        }

        private IntPtr _denseFlowPtr;
        private IntPtr _algorithmPtr;

        /// <summary>
        /// Create an instance of DIS optical flow algorithm.
        /// </summary>
        /// <param name="preset">Algorithm preset</param>
        public DISOpticalFlow(Preset preset = Preset.Fast)
        {
            _ptr = CvInvoke.cveDISOpticalFlowCreate(preset, ref _denseFlowPtr, ref _algorithmPtr);
        }

        /// <summary>
        /// Release the unmanaged memory associated with this Optical flow algorithm.
        /// </summary>
        protected override void DisposeObject()
        {
            if (IntPtr.Zero != _ptr)
            {
                CvInvoke.cveDISOpticalFlowRelease(ref _ptr);
            }
            _algorithmPtr = IntPtr.Zero;
            _denseFlowPtr = IntPtr.Zero;
        }

        public IntPtr AlgorithmPtr { get { return _algorithmPtr; } }
        public IntPtr DenseOpticalFlowPtr { get { return _denseFlowPtr; } }
    }

    public static partial class CvInvoke
    {
        [DllImport(ExternLibrary, CallingConvention = CvInvoke.CvCallingConvention)]
        internal static extern IntPtr cveDISOpticalFlowCreate(DISOpticalFlow.Preset preset, ref IntPtr denseFlow, ref IntPtr algorithm);

        [DllImport(ExternLibrary, CallingConvention = CvInvoke.CvCallingConvention)]
        internal static extern void cveDISOpticalFlowRelease(ref IntPtr flow);
    }
}