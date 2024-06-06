namespace Skyline.DataMiner.CICD.Validators.Protocol.Tests.Protocol.Params.Param.Display.Decimals.CheckDecimalsTag
{
    using System;
    using System.Collections.Generic;
    
    using Skyline.DataMiner.CICD.Validators.Common.Interfaces;
    using Skyline.DataMiner.CICD.Validators.Common.Model;
    using Skyline.DataMiner.CICD.Validators.Protocol.Common;
    using Skyline.DataMiner.CICD.Validators.Protocol.Common.Attributes;
    using Skyline.DataMiner.CICD.Validators.Protocol.Common.Extensions;
    using Skyline.DataMiner.CICD.Validators.Protocol.Interfaces;

    [Test(CheckId.CheckDecimalsTag, Category.Param)]
    internal class CheckDecimalsTag : IValidate/*, ICodeFix, ICompare*/
    {
        // Please comment out the interfaces that aren't used together with the respective methods.

        public List<IValidationResult> Validate(ValidatorContext context)
        {
            List<IValidationResult> results = new List<IValidationResult>();

            foreach (var param in context.EachParamWithValidId())
            {
                var displayTag = param.Display;

                // Early return pattern. Only check when there is a Display tag.
                if (displayTag == null) continue;

                // Only check number types.
                if (!param.IsNumber()) continue;

                // Only check if date or datetime parameter
                if (!param.IsDateTime()) continue;

                // Verify valid decimals.
                var decimalsTag = param.Display?.Decimals;
                if (decimalsTag?.Value != 8)
                {
                    
                    results.Add(Error.InvalidDecimalsForDatetime(test:this, referenceNode:param, positionNode:decimalsTag, param.Id.RawValue));
                }
            }

            return results;
        }


        ////        public ICodeFixResult Fix(CodeFixContext context)
        ////        {
        ////            CodeFixResult result = new CodeFixResult();

        ////            switch (context.Result.ErrorId)
        ////            {

        ////                default:
        ////                    result.Message = $"This error ({context.Result.ErrorId}) isn't implemented.";
        ////                    break;
        ////            }

        ////            return result;
        ////        }

        ////        public List<IValidationResult> Compare(MajorChangeCheckContext context)
        ////        {
        ////            List<IValidationResult> results = new List<IValidationResult>();

        ////            return results;
        ////        }
    }
}