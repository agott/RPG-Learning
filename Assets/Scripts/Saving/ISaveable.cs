namespace RPG.Saving
{
    interface ISaveable
    {
        public object CaptureState();

        public void RestoreState(object state);
    }
}