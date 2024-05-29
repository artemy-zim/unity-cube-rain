public class BombSpawnerView : SpawnerView<Bomb>
{
    protected override void UpdateActive(int count)
    {
        ActiveText.text = $"���������� �������� ���� �� �����: {count}";
    }

    protected override void UpdateCreated(int count)
    {
        CreatedText.text = $"���������� ��������� ���� �� ��� �����: {count}";
    }
}
