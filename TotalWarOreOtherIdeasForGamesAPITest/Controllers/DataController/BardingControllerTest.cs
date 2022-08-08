using AutoFixture;
using NUnit;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TotalWarOreOtherIdeasForGames.Controllers.DataController;
using TotalWarDLA.Models;

namespace TotalWarOreOtherIdeasForGamesAPITest.Controllers.DataController
{
    public class BardingControllerTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<BardingController> _controller;
        private readonly TotalWarWanaBeContext _context;
    }
}
