using PatientInfo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientInfo.Repositories
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllPatientsAsync();
        Task<Patient> GetPatientByIdAsync(int patientId);
        Task<int> AddPatientAsync(Patient patient);
        Task<int> UpdatePatientAsync(Patient patient);
        Task<int> DeletePatientAsync(int patientId);
    }
}