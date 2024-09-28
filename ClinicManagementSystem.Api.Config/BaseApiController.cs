using ClinicManagementSystem.Shared.Notifications;
using ClinicManagementSystem.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Api.Config;

public class BaseApiController : Controller
{
    protected readonly IMediator Mediator;
    protected readonly IDomainNotification Notifications;

    public BaseApiController(IDomainNotification notifications, IMediator mediator)
    {
        Notifications = notifications;
        Mediator = mediator;
    }

    [NonAction]
    protected IActionResult CreateResponse<T>(T data = default(T))
    {
        if (!Notifications.HasNotifications()) return Ok(EnvelopDataResult<T>.Ok(data));
        return BadRequest(EnvelopResult.Fail(Notifications.Notify()));
    }
}