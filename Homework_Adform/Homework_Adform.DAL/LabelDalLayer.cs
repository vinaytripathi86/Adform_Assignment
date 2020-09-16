using AutoMapper;
using Homework_Adform.CommonLibrary.Contracts.DAL;
using Homework_Adform.CommonLibrary.Models.DBModels;
using Homework_Adform.CommonLibrary.Models.DTOs;
using Homework_Adform.DAL.DBContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework_Adform.DAL
{
    /// <summary>
    /// Implemenation of ILabelDalLayer contract.
    /// </summary>
    public class LabelDalLayer : ILabelDalLayer
    {
        private readonly HomeworkDBContext _dbContext;
        private readonly IMapper _mapper;

        /// <summary>
        /// Create new instance of <see cref="LabelDalLayer"/> class.
        /// </summary>
        /// <param name="mapper">Auto mapper.</param>
        /// <param name="dBContext">Db context.</param>
        public LabelDalLayer(IMapper mapper, HomeworkDBContext dBContext)
        {
            _mapper = mapper;
            _dbContext = dBContext;
        }

        /// <summary>
        /// Assign label to item.
        /// </summary>
        /// <param name="assignLabel">Parameter of type AssignLabelDto.</param>
        /// <returns>True if assigned, false otherwise.</returns>
        public async Task<bool> AssignLabelToItem(AssignLabelDto assignLabel)
        {
            var item = await _dbContext.TodoItems.FirstOrDefaultAsync(p => p.CreatedBy == assignLabel.CreatedBy && p.Id == assignLabel.EntityId);
            if (null == item)
                return false;

            item.LabelId = assignLabel.Id;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Assign label to list.
        /// </summary>
        /// <param name="assignLabel">Parameter of type AssignLabelDto.</param>
        /// <returns>True if assigned, false otherwise.</returns>
        public async Task<bool> AssignLabelToList(AssignLabelDto assignLabel)
        {
            var item = await _dbContext.TodoLists.FirstOrDefaultAsync(p => p.CreatedBy == assignLabel.CreatedBy && p.Id == assignLabel.EntityId);
            if (null == item)
                return false;

            item.LabelId = assignLabel.Id;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Delete label.
        /// </summary>
        /// <param name="labelId">Label id.</param>
        /// <param name="userId">User id.</param>
        /// <returns>Returns deleted label.</returns>
        public async Task<LabelDto> DeleteLabel(long labelId, long userId)
        {
            var dbData = await _dbContext.Labels.Where(p => p.CreatedBy == userId && p.Id == labelId).SingleOrDefaultAsync();
            if (null == dbData)
                return null;

            _dbContext.Labels.Remove(dbData);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<LabelDto>(dbData);
        }

        /// <summary>
        /// Get label by id.
        /// </summary>
        /// <param name="labelId">label id.</param>
        /// <param name="userId">user id.</param>
        /// <returns>Label details of type LabelDto.</returns>
        public async Task<LabelDto> GetLabel(long labelId, long userId)
        {
            var dbData = await _dbContext.Labels.Where(p => p.Id == labelId && p.CreatedBy == userId).SingleOrDefaultAsync();
            if (null == dbData) return null;
            return _mapper.Map<LabelDto>(dbData);
        }

        /// <summary>
        /// Get all labels.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>Returns all labels for user.</returns>
        public async Task<List<LabelDto>> GetLabels(long userId)
        {
            var dbLabels = await _dbContext.Labels.Where(p => p.CreatedBy == userId).ToListAsync();
            if (null == dbLabels || dbLabels.Count == 0) return null;
            return _mapper.Map<List<LabelDto>>(dbLabels);
        }

        /// <summary>
        /// Add label.
        /// </summary>
        /// <param name="label">Label details.</param>
        /// <returns>Returns added label.</returns>
        public async Task<LabelDto> Add(CreateLabelDTO label)
        {
            LabelDbModel lbl = _mapper.Map<LabelDbModel>(label);
            _dbContext.Labels.Add(lbl);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<LabelDto>(lbl);
        }
    }
}
