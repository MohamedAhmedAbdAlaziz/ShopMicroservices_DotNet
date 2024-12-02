﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuidingBlocks.CQRS;

public interface IOuery<out TResponse>  
    : IRequest<TResponse>
    where TResponse:notnull
{

}