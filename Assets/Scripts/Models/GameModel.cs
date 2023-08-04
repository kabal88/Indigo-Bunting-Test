namespace Models
{
    public class GameModel
    {
        public int LevelGeneratorId { get; }
        public int InputDescriptionId { get; }
        public int CameraDescriptionId { get; }
        
        public int UnitDescriptionId { get; }

        public GameModel(int levelGeneratorId,
            int inputDescriptionId,
            int cameraDescriptionId,
            int unitDescriptionId)
        {
            LevelGeneratorId = levelGeneratorId;
            InputDescriptionId = inputDescriptionId;
            CameraDescriptionId = cameraDescriptionId;
            UnitDescriptionId = unitDescriptionId;
        }
        
    }
}