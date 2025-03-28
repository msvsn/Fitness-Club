using AutoMapper;
using FitnessClub.BLL.DTO;
using FitnessClub.BLL.Services.Interfaces;
using FitnessClub.DAL.Models;
using FitnessClub.DAL.UnitOfWork;
using System;
using System.Linq.Expressions;

namespace FitnessClub.BLL.Services
{
    public class ClassTypeService : AutoMapperGenericService<ClassType, ClassTypeDTO>, IClassTypeService
    {
        public ClassTypeService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override Expression<Func<ClassType, bool>> GetByIdPredicate(int id)
        {
            return classType => classType.Id == id;
        }
    }
} 