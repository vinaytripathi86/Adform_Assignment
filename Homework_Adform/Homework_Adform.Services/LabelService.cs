using Homework_Adform.CommonLibrary.Contracts.DAL;
using Homework_Adform.CommonLibrary.Contracts.Services;
using Homework_Adform.CommonLibrary.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homework_Adform.Services
{
    /// <summary>
    /// Implemenation of ILabelService contract.
    /// </summary>
    public class LabelService : ILabelService
    {
        private readonly ILabelDalLayer _labelDalLayer;

        /// <summary>
        /// Create new instance of <see cref="LabelService"/> class.
        /// </summary>
        /// <param name="labelDalLayer">Label dal layer.</param>
        public LabelService(ILabelDalLayer labelDalLayer)
        {
            _labelDalLayer = labelDalLayer;
        }

        /// <summary>
        /// Assign label to item.
        /// </summary>
        /// <param name="assignLabel">Parameter of type AssignLabelDto.</param>
        /// <returns>True if assigned, false otherwise.</returns>
        public async Task<bool> AssignLabelToItem(AssignLabelDto assignLabel) => await _labelDalLayer.AssignLabelToItem(assignLabel);

        /// <summary>
        /// Assign label to list.
        /// </summary>
        /// <param name="assignLabel">Parameter of type AssignLabelDto.</param>
        /// <returns>True if assigned, false otherwise.</returns>
        public async Task<bool> AssignLabelToList(AssignLabelDto assignLabel) => await _labelDalLayer.AssignLabelToList(assignLabel);

        /// <summary>
        /// Delete label.
        /// </summary>
        /// <param name="labelId">Label id.</param>
        /// <param name="userId">User id.</param>
        /// <returns>Returns deleted label.</returns>
        public async Task<LabelDto> DeleteLabel(DeleteLabelDto deleteLabel) => await _labelDalLayer.DeleteLabel(deleteLabel.Id, deleteLabel.CreatedBy);

        /// <summary>
        /// Get label by id.
        /// </summary>
        /// <param name="labelId">label id.</param>
        /// <param name="userId">user id.</param>
        /// <returns>Label details of type LabelDto.</returns>
        public async Task<LabelDto> GetLabel(long labelId, long userId) => await _labelDalLayer.GetLabel(labelId, userId);

        /// <summary>
        /// Get all labels.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>Returns all labels for user.</returns>
        public async Task<List<LabelDto>> GetLabels(long userId) => await _labelDalLayer.GetLabels(userId);

        /// <summary>
        /// Add label.
        /// </summary>
        /// <param name="label">Label details.</param>
        /// <returns>Returns added label.</returns>
        public async Task<LabelDto> Add(CreateLabelDTO label) => await _labelDalLayer.Add(label);
    }
}
