import { SearchBar } from '../components/SearchBar';
import { PlaceMap } from '../components/PlaceMap';
import { PlaceList } from '../components/PlaceList';

export function HomePage() {
  return (
    <div className="flex flex-col h-[calc(100vh-57px)]">
      <div className="p-4">
        <SearchBar />
      </div>
      <div className="flex flex-1 min-h-0">
        <aside className="w-80 border-r border-gray-200 p-4 overflow-y-auto hidden lg:block">
          <PlaceList />
        </aside>
        <main className="flex-1">
          <PlaceMap />
        </main>
      </div>
    </div>
  );
}
