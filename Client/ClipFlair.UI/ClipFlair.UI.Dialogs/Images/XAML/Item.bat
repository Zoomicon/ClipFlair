@echo off

echo:
echo ^<!-- %1 --^>
echo:
echo ^<ControlTemplate x:Key="%1"^>
echo ^<Viewbox^>
type %1
echo:
echo ^</Viewbox^>
echo ^</ControlTemplate^>
