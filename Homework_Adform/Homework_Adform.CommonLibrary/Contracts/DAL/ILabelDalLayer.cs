using Homework_Adform.CommonLibrary.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homework_Adform.CommonLibrary.Contracts.DAL
{
    /// <summary>
    /// Contract for labels.
    /// </summary>
    public interface ILabelDalLayer
    {
        /// <summary>
        /// Assign label to item.
        /// </summary>
        /// <param name="assignLabel">Parameter of type AssignLabelDto.</param>
        /// <returns>True if assigned, false otherwise.</returns>
        Task<bool> AssignLabelToItem(AssignLabelDto assignLabel);

        /// <summary>
        /// Assign label to list.
        /// </summary>
        /// <param name="assignLabel">Parameter of type AssignLabelDto.</param>
        /// <returns>True if assigned, false otherwise.</returns>
        Task<bool> AssignLabelToList(AssignLabelDto assignLabel);

        /// <summary>
        /// Delete label.
        /// </summary>
        /// <param name="labelId">Label id.</param>
        /// <param name="userId">User id.</param>
        /// <returns>Returns deleted label.</returns>
        Task<LabelDto> DeleteLabel(long labelId, long userId);

        /// <summary>
        /// Get label by id.
        /// </summary>
        /// <param name="labelId">label id.</param>
        /// <param name="userId">user id.</param>
        /// <returns>Label details of type LabelDto.</returns>
        Task<LabelDto> GetLabel(long labelId, long userId);

        /// <summary>
        /// Get all labels.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>Returns all labels for user.</returns>
        Task<List<LabelDto>> GetLabels(long userId);

        /// <summary>
        /// Add label.
        /// </summary>
        /// <param name="label">Label details.</param>
        /// <returns>Returns added label.</returns>
        Task<LabelDto> Add(CreateLabelDTO label);
    }
}
