// <copyright file="FunctionCommonTest.cs">Copyright ©  2010</copyright>
using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ry.Function;

namespace Ry.Function.Tests
{
    /// <summary>此类包含 FunctionCommon 的参数化单元测试</summary>
    [PexClass(typeof(FunctionCommon))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class FunctionCommonTest
    {
        /// <summary>测试 GetLocalDiskAlignment() 的存根</summary>
        [PexMethod]
        public string GetLocalDiskAlignmentTest()
        {
            string result = FunctionCommon.GetLocalDiskAlignment();
            return result;
            // TODO: 将断言添加到 方法 FunctionCommonTest.GetLocalDiskAlignmentTest()
        }
    }
}
