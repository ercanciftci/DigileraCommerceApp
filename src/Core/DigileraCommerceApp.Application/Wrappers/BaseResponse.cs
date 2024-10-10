using System;
using System.Collections.Generic;
using System.Text;

namespace DigileraCommerceApp.Application.Wrappers;

public class BaseResponse
{
    public int Id { get; set; }

    public string Message { get; set; }

    public bool IsSuccess { get; set; } = true;
}
