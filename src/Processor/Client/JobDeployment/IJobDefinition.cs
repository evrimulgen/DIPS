﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Client.JobDeployment
{
    public interface IJobDefinition
    {
        PipelineDefinition GetAlgorithms();

        IEnumerable<JobInput> GetInputs();
    }
}
